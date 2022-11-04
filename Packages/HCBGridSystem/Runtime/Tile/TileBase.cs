using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;


namespace HCB.GridSystem
{
    public abstract class TileBase : MonoBehaviour, ITile
    {
        public Transform T => transform;

        [ReadOnly][SerializeField] private TileInitializeData _tileInitialize;
        public TileInitializeData TileInitializeData { get => _tileInitialize; set => _tileInitialize = value; }
        public TileSaveData TileSaveData { get; private set; }
        public bool IsOccupied { get; protected set; }
        public bool IsAvailable { get => !IsOccupied && TileInitializeData.TileType == TileType.Available; }
        public IPlaceable LastInteractedItem { get; private set; }
        public IPlaceable PlacedItem { get; protected set; }
        public string TileID => SceneManager.GetActiveScene().name + TileInitializeData.Grid.x.ToString() + TileInitializeData.Grid.y.ToString();      

        //Editor Event        
        [SerializeField] private UnityEvent<TileInitializeData> OnInitialized = new UnityEvent<TileInitializeData>();

        [HideInInspector]
        public UnityEvent OnInteractionStart = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnInteractionEnd = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnItemPlaced = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnItemRemoved = new UnityEvent();
        [HideInInspector]
        public UnityEvent OnTileSaveDataLoaded = new UnityEvent();

        protected virtual void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            TileManager.Instance.AddTile(this);
        }

        protected virtual void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            TileManager.Instance.RemoveTile(this);
        }

        //Invokes in editor when its created
        public virtual void Initialize(TileInitializeData tileInitializeData)
        {
            TileInitializeData = tileInitializeData;
            OnInitialized.Invoke(tileInitializeData);
        }

        public virtual void StartInteraction(IPlaceable placeable)
        {
            LastInteractedItem = placeable;
            OnInteractionStart.Invoke();
        }

        public virtual void EndInteraction(IPlaceable placeable)
        {
            if (LastInteractedItem == placeable)
                LastInteractedItem = null;

            OnInteractionEnd.Invoke();
        }

        public virtual void PlaceItem(IPlaceable placeable)
        {
            if (IsOccupied || PlacedItem == placeable)
                return;

            IsOccupied = true;
            PlacedItem = placeable;
            OnItemPlaced.Invoke();
        }

        public virtual void RemoveItem(IPlaceable placeable)
        {
            if (!IsOccupied || PlacedItem != placeable)
                return;

            IsOccupied = false;
            PlacedItem = null;
            OnItemRemoved.Invoke();
        }

        public virtual void LoadTileData(TileSaveData tileSaveData)
        {
            TileSaveData = tileSaveData;
            OnTileSaveDataLoaded.Invoke();
        }
    }
}
