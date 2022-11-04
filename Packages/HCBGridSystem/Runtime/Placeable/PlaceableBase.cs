using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using System;
namespace HCB.GridSystem
{
    public abstract class PlaceableBase : SelectableBase, IPlaceable
    {
        private IPlaceable _placeable;
        public IPlaceable Placeable => _placeable == null ? _placeable = GetComponent<IPlaceable>() : _placeable;

        public ITile LastPlacedPivotTile { get; protected set; }
        public ITile LastInteractedPivotTile { get; protected set; }

        private List<ITile> _placedTiles = new List<ITile>();
        public List<ITile> LastPlacedTiles { get => _placedTiles; protected set => _placedTiles = value; }

        private List<ITile> _interactedTiles = new List<ITile>();
        public List<ITile> LastInteractedTiles { get => _interactedTiles; protected set => _interactedTiles = value; }
        
        public bool IsActive { get; protected set; }
        public bool IsPlaced { get; protected set; }

        public Transform PivotPoint => _pivotPoint;
        public List<Vector2Int> GridPoints { get => _gridPoints; protected set => _gridPoints = value; }

        [Header("Placeable")]
        [SerializeField] protected LayerMask _tileLayer;
        [SerializeField] protected Transform _pivotPoint;        
        [Space(10)]
        [SerializeField] List<Vector2Int> _gridPoints = new List<Vector2Int>();        

        protected const float SPHERECAST_DISTANCE = 50f;
        protected const float SPHERECAST_RADIUS = 0.05f;

        protected const float MOVEMENT_DURATION = 0.15f;

        protected string _movementTweenID;
        protected Vector3 _initialLocalPosition;

        [HideInInspector]
        public UnityEvent OnPlaced = new UnityEvent();

        protected override void Awake()
        {
            base.Awake();
            Setup();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            OnSelected.AddListener(() => DOTween.Kill(_movementTweenID));
            OnDeselected.AddListener(CheckPlacement);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            OnSelected.RemoveListener(() => DOTween.Kill(_movementTweenID));
            OnDeselected.RemoveListener(CheckPlacement);

            DOTween.Kill(_movementTweenID);
        }

        protected virtual void OnDestroy() 
        {
            UnPlace();
        }

        protected virtual void FixedUpdate()
        {
            if (!IsSelected)
                return;

            CheckInteraction();
        }

        public override bool Deselect()
        {
            if (!base.Deselect())
                return false;

            EndAllInteractions();
            return true;
        }

        public virtual void UnPlace()
        {
            foreach (var tile in LastPlacedTiles)
            {
                tile.RemoveItem(this);
            }
        }

        protected virtual void KillTweens()
        {
            DOTween.Kill(_movementTweenID);
        }

        private void CheckInteraction()
        {
            Vector3 origin = PivotPoint.position + Vector3.up;
            if (Physics.SphereCast(origin, SPHERECAST_RADIUS, Vector3.down, out RaycastHit hit, SPHERECAST_DISTANCE, _tileLayer))
            {
                ITile tile = hit.collider.GetComponentInParent<ITile>();
                if (tile != null)
                {
                    LastInteractedPivotTile = tile;
                    List<ITile> interactedTiles = GetNeighborTiles(LastInteractedPivotTile);
                    SetInteractedTiles(interactedTiles);
                }
            }
            else
            {
                LastInteractedPivotTile = null;
                SetInteractedTiles(null);
            }
        }

        private void CheckPlacement()
        {
            if (CanPlace())
            {
                Place(LastInteractedPivotTile);
            }

            else if (IsActive && LastPlacedPivotTile != null)
            {
                SetPosition(GetPositionWithPivotOffset(LastPlacedPivotTile.T.position));
            }

            else if (IsActive)
            {
                SetLocalPosition(_initialLocalPosition);
            }
        }

        protected virtual void Place(ITile pivotTile)
        {
            //Remove Item
            UnPlace();

            //Place Item
            foreach (var tile in LastInteractedTiles)
            {
                tile.PlaceItem(this);
            }

            LastPlacedPivotTile = pivotTile;
            LastPlacedTiles = LastInteractedTiles;

            transform.SetParent(null);
            SetPosition(GetPositionWithPivotOffset(LastPlacedPivotTile.T.position));
            OnPlaced.Invoke();
        }      

        private void SetInteractedTiles(List<ITile> interactedTiles)
        {
            interactedTiles ??= new List<ITile>();

            //End Interactions
            foreach (var tile in LastInteractedTiles)
            {
                if (interactedTiles.Contains(tile))
                    continue;

                tile.EndInteraction(this);
            }

            //Start Interactions
            foreach (var tile in interactedTiles)
            {
                if (LastInteractedTiles.Contains(tile))
                    continue;

                tile.StartInteraction(this);
            }

            LastInteractedTiles = interactedTiles;
        }

        private void EndAllInteractions()
        {
            foreach (var tile in LastInteractedTiles)
            {
                tile.EndInteraction(this);
            }

            LastInteractedTiles = new List<ITile>();
        }

        protected List<ITile> GetNeighborTiles(ITile pivotTile)
        {
            List<ITile> tiles = new List<ITile>();

            for (int i = 0; i < GridPoints.Count; i++)
            {
                int x = pivotTile.TileInitializeData.Grid.x + GridPoints[i].x;
                int y = pivotTile.TileInitializeData.Grid.y + GridPoints[i].y;

                ITile tile = TileManager.Instance.GetTile(x, y);
                if (tile != null && !tiles.Contains(tile))
                {
                    tiles.Add(tile);
                }
            }
            return tiles;
        }

        private bool CanPlace()
        {
            if (!IsActive)
                return false;

            if (LastInteractedPivotTile == null)
                return false;            

            bool canPlace = true;
            for (int i = 0; i < GridPoints.Count; i++)
            {
                int x = LastInteractedPivotTile.TileInitializeData.Grid.x + GridPoints[i].x;
                int y = LastInteractedPivotTile.TileInitializeData.Grid.y + GridPoints[i].y;

                ITile tile = TileManager.Instance.GetTile(x, y);
                if (tile == null || (!tile.IsAvailable && tile.PlacedItem != Placeable))
                {
                    canPlace = false;
                    break;
                }
            }

            return canPlace;
        }

        public void SetPosition(Vector3 worldPosition, Action onComplete = null)
        {
            DOTween.Kill(_movementTweenID);
            transform.DOMove(worldPosition, MOVEMENT_DURATION).SetEase(Ease.Linear).SetId(_movementTweenID).OnComplete(() => onComplete?.Invoke());
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            DOTween.Kill(_movementTweenID);
            transform.DOLocalMove(localPosition, MOVEMENT_DURATION).SetEase(Ease.Linear).SetId(_movementTweenID);
        }

        private Vector3 GetPositionWithPivotOffset(Vector3 postition)
        {
            Vector3 offset = transform.position - PivotPoint.position;
            Vector3 target = postition + offset;
            return target;
        }

        private void Setup()
        {
            _movementTweenID = GetInstanceID() + "MovementTweenID";
            _initialLocalPosition = transform.localPosition;
        }
    }
}
