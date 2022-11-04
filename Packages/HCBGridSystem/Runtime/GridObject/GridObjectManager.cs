using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class GridObjectManager : Singleton<GridObjectManager>
    {
        private List<GridObject> _gridObjects = new List<GridObject>();
        public List<GridObject> GridObjects { get => _gridObjects; private set => _gridObjects = value; }

        [SerializeField] private bool _saveGridObjects;

        private void OnEnable()
        {
            GridSystemEventManager.OnTileDataLoaded.AddListener(LoadGridObjects);
        }

        private void OnDisable()
        {
            GridSystemEventManager.OnTileDataLoaded.RemoveListener(LoadGridObjects);
        }

        private void Start()
        {
            SubscribeGridSystemManager();
        }

        public void AddGridObject(GridObject gridObject)
        {
            if (GridObjects.Contains(gridObject))
                return;

            GridObjects.Add(gridObject);
        }

        public void RemoveGridObject(GridObject gridObject)
        {
            if (!GridObjects.Contains(gridObject))
                return;

            GridObjects.Remove(gridObject);
        }

        private bool CreateGridObject(GridObjectData gridObjectData, ITile tile)
        {
            if (!tile.IsAvailable)
                return false;

            Vector3 spawnPosition = tile.T.position; 

            GridObject gridObject = Instantiate(gridObjectData.Prefab, spawnPosition, Quaternion.identity).GetComponentInChildren<GridObject>();
            gridObject.InitializeFromSaveData(tile);

            GridSystemEventManager.OnGridObjectCreated.Invoke();
            return true;
        }

        private void SaveGridObjects()
        {
            if (!_saveGridObjects)
                return;

            List<GridObjectSaveData> gridObjectSaveData = new List<GridObjectSaveData>();
            foreach (var gridObject in GridObjects)
            {
                if (gridObject.LastPlacedPivotTile == null)
                    continue;

                gridObjectSaveData.Add(new GridObjectSaveData(gridObject.GridObjectData.ID, gridObject.LastPlacedPivotTile.TileID));
            }
            GridSystemManager.Instance.GridSystemData.GridObjectSaveDatas = gridObjectSaveData;
        }

        private void LoadGridObjects()
        {
            foreach (var gridObjectSaveData in GridSystemManager.Instance.GridSystemData.GridObjectSaveDatas)
            {
                if (!GridObjectDataManager.Instance.GridObjectDatasByID.ContainsKey(gridObjectSaveData.GridObjectID))
                    continue;

                ITile tile = TileManager.Instance.GetTile(gridObjectSaveData.LastPlacedPivotTileID);
                if (tile == null)
                    continue;

                GridObjectData gridObjectData = GridObjectDataManager.Instance.GridObjectDatasByID[gridObjectSaveData.GridObjectID];
                CreateGridObject(gridObjectData, tile);
            }

            GridSystemEventManager.OnGridObjectDataLoaded.Invoke();
        }

        private void SubscribeGridSystemManager()
        {
            GridSystemManager.Instance.OnApplicationPauseActions.Add(() => SaveGridObjects());
            GridSystemManager.Instance.OnApplicationQuitActions.Add(() => SaveGridObjects());
        }
    }
}
