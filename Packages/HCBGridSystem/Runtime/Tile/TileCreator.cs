using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
using UnityEditor;
#endif

namespace HCB.GridSystem.Samples
{
    public class TileCreator : MonoBehaviour
    {
        public Transform TileParent;
        [Space(10)]
        [InlineEditor(InlineEditorModes.GUIOnly)]
        public TileCreateData TileCreateData;

        private int row { get { return TileCreateData.Row; } }
        private int column { get { return TileCreateData.Column; } }

        [Button]
        private void CreateTiles()
        {
#if UNITY_EDITOR

            if (PrefabStageUtility.GetPrefabStage(gameObject) != null)
            {
                Debug.LogError("You can not create tiles in prefab mode");
                return;
            }

            ClearTiles();

            Vector3 centerPosition = Vector3.Scale(new Vector3(column / 2f - 0.5f, 0f, row / 2f - 0.5f), new Vector3(TileCreateData.CellSize.x, 0f, TileCreateData.CellSize.y)) + new Vector3(TileCreateData.Padding.x * (column - 1) / 2f, 0, TileCreateData.Padding.y * (row - 1) / 2f);
            Vector3 offsetPosition = (TileParent.position - centerPosition);
            int lastRowIndex = row - 1;

            for (int z = 0; z < row; z++)
            {
                for (int x = 0; x < column; x++)
                {                    
                    Vector3 spawnPosition = Vector3.Scale(new Vector3(x, 0, lastRowIndex - z), new Vector3(TileCreateData.CellSize.x, 0f, TileCreateData.CellSize.y)) + Vector3.Scale(new Vector3(x, 0, lastRowIndex - z), new Vector3(TileCreateData.Padding.x, 0f, TileCreateData.Padding.y)) + offsetPosition;
                    TileBase tile = InstantiateTile(spawnPosition, " " + x + "" + z);                           

                    TileInitializeData tileInitializeData = TileCreateData.Tiles[x,z];

                    bool isOffset = (x + z) % 2 != 1;

                    tileInitializeData.IsOffset = isOffset;
                    tileInitializeData.Size = TileCreateData.CellSize;

                    tile.Initialize(tileInitializeData);                  
                }
            }

#endif
        }

#if UNITY_EDITOR
        private TileBase InstantiateTile(Vector3 spawnPosition, string nameSuffix) 
        {

            TileBase tile = (PrefabUtility.InstantiatePrefab(TileCreateData.TilePrefab) as GameObject).GetComponent<TileBase>();
            tile.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            tile.transform.SetParent(TileParent);
            tile.gameObject.name += nameSuffix;
            return tile;
        }
#endif

        private TileInitializeData GetTileInitializeData(Vector2Int grid) 
        {
            TileInitializeData tileInitializeData = null;
            foreach (var tileData in TileCreateData.Tiles)
            {
                if (tileData.Grid == grid)
                {
                    tileInitializeData = tileData;
                    break;
                }
            }

            if (tileInitializeData == null)
            {
                tileInitializeData = new TileInitializeData(grid);
            }

            return tileInitializeData;
        }         

        [Button]
        private void ClearTiles()
        {
            List<Transform> createdTiles = new List<Transform>(TileParent.GetComponentsInChildren<Transform>());
            createdTiles.Remove(TileParent);

            for (int i = 0; i < createdTiles.Count; i++)
            {
                if (createdTiles[i] != null)
                    DestroyImmediate(createdTiles[i].gameObject);
            }
            createdTiles.Clear();
        }
    }
}
