using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;

namespace HCB.GridSystem
{
    public class GridObject : PlaceableBase
    {
        [Header("Grid Object")]
        [SerializeField] private GridObjectData _gridObjectData;
        public GridObjectData GridObjectData => _gridObjectData;

        protected override void Awake()
        {
            base.Awake();
            Activate();
        }

        protected override void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            GridObjectManager.Instance.AddGridObject(this);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            GridObjectManager.Instance.RemoveGridObject(this);
            base.OnDisable();
        }

        public void InitializeFromSaveData(ITile lastPlacedPivotTile) 
        {
            InitialPlacement(lastPlacedPivotTile);
        }       

        private void InitialPlacement(ITile lastPlacedPivotTile) 
        {
            LastPlacedPivotTile = lastPlacedPivotTile;
            Place(LastPlacedPivotTile);

            for (int i = 0; i < GridPoints.Count; i++)
            {
                int x = LastPlacedPivotTile.TileInitializeData.Grid.x + GridPoints[i].x;
                int z = LastPlacedPivotTile.TileInitializeData.Grid.y + GridPoints[i].y;

                ITile tile = TileManager.Instance.GetTile(x, z);
                if (tile != null)
                {
                    LastPlacedTiles.Add(tile);
                    tile.PlaceItem(this);
                }
            }
        }

        private void Activate()
        {
            IsActive = true;
            CanSelectable = true;
        }
    }
}
