using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using HCB.GridSystem.Editor;
using UnityEditor;
#endif

namespace HCB.GridSystem
{
    public class TileCreateData : SerializedScriptableObject
    {
        [OnValueChanged("Initialize")]
        [Min(1)] public int Row = 5;
        [OnValueChanged("Initialize")]
        [Min(1)] public int Column = 4;

        public Vector2 CellSize = Vector2.one;
        public Vector2 Padding = Vector2.one * 0.2f;

        public GameObject TilePrefab;

        [TableMatrix(DrawElementMethod = "DrawCell")]
        public TileInitializeData[,] Tiles;

#if UNITY_EDITOR
        [SerializeField]
        [HideInInspector]
        private GridEditorIconData _gridEditorIconData;
        private GridEditorIconData gridEditorIconData { get => _gridEditorIconData == null ? _gridEditorIconData = GetGridEditorIconData() : _gridEditorIconData; }
#endif

#if UNITY_EDITOR
        private static GridEditorIconData GetGridEditorIconData()
        {
            return UnityEditor.AssetDatabase.FindAssets("t:GridEditorIconData")
                .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
                .Select(x => UnityEditor.AssetDatabase.LoadAssetAtPath<GridEditorIconData>(x)).FirstOrDefault();
        }
#endif

        private void Initialize()
        {
            Tiles = new TileInitializeData[Column, Row];
            for (int z = 0; z < Row; z++)
            {
                for (int x = 0; x < Column; x++)
                {
                    Tiles[x, z] = new TileInitializeData(new Vector2Int(x, z));
                }
            }
        }

        public void SaveTileData(TileInitializeData tileInitializeData)
        {
            Tiles[tileInitializeData.Grid.x, tileInitializeData.Grid.y] = tileInitializeData;
        }

#if UNITY_EDITOR
        private TileInitializeData DrawCell(Rect rect, TileInitializeData tileInitializeData)
        {
            if(Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                TileInitializeDataSelector tileInitializeDataSelector = TileInitializeDataSelector.GetWindow<TileInitializeDataSelector>();
                tileInitializeDataSelector.Initialize(tileInitializeData, this);
            }

            EditorGUI.DrawPreviewTexture(rect, GetRectTexture(tileInitializeData), null, ScaleMode.ScaleToFit);
            return tileInitializeData;
        }
#endif

        private Color GetRectColor(TileInitializeData tileInitializeData)
        {
            Color color = Color.black;

            if (tileInitializeData == null)
                color = Color.red;

            return color;
        }

#if UNITY_EDITOR
        private Texture2D GetRectTexture(TileInitializeData tileInitializeData)
        {
            switch (tileInitializeData.TileType)
            {
                case TileType.Invisable:
                    return gridEditorIconData.PreviewImages[GridEditorIconIDHolder.InvisableTile];
                case TileType.Unuseable:
                    return gridEditorIconData.PreviewImages[GridEditorIconIDHolder.UnuseableTile];
                case TileType.Available:
                    return gridEditorIconData.PreviewImages[GridEditorIconIDHolder.AvailableTile];
                default:
                    return gridEditorIconData.PreviewImages[GridEditorIconIDHolder.InvisableTile];
            }

        }
#endif
    }

    public enum TileType {Invisable, Unuseable, Available }

    [System.Serializable]
    public class TileInitializeData
    {
        [EnumToggleButtons]
        public TileType TileType = TileType.Available;

        [HideInInspector] public Vector2Int Grid;
        [HideInInspector] public bool IsOffset;
        [HideInInspector] public Vector2 Size;

        public TileInitializeData(Vector2Int grid) 
        {
            Grid = grid;
        }
    }
}
