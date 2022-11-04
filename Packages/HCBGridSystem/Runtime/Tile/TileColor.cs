using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class TileColor : MonoBehaviour
    {
        private TileBase _tile;
        private TileBase Tile => _tile == null ? _tile = GetComponentInParent<TileBase>() : _tile;

        private SpriteRenderer _renderer;
        private SpriteRenderer SpriteRenderer => _renderer == null ? _renderer = GetComponentInChildren<SpriteRenderer>() : _renderer;

        private float DefaultAlpha => SpriteRenderer.color.a;

        [SerializeField] private TileColorData _colorData;

        private void OnEnable()
        {            
            Tile.OnInteractionStart.AddListener(SetHighlightColor);
            Tile.OnInteractionEnd.AddListener(() => SetColor(GetDefaultColor()));
        }

        private void OnDisable()
        {            
            Tile.OnInteractionStart.RemoveListener(SetHighlightColor);
            Tile.OnInteractionEnd.RemoveListener(() => SetColor(GetDefaultColor()));
        }

        //Listens Tile Editor Event   
        public void UpdateTileColor(TileInitializeData tileInitializeData)
        {
            SetColor(GetDefaultColor());
        }

        public void SetColor(Color color)
        {            
            SpriteRenderer.color = color;
        }

        private void SetHighlightColor()
        {
            SetColor(GetHighlightColor());
        }

        private Color GetDefaultColor()
        {
            Color color;

            switch (Tile.TileInitializeData.TileType)
            {
                case TileType.Invisable:
                    color = _colorData.InvisableDefaultColor;
                    break;
                case TileType.Unuseable:
                    color = Tile.TileInitializeData.IsOffset ? _colorData.DeactiveOffsetColor : _colorData.DeactiveDefaultColor;
                    break;
                case TileType.Available:
                    color = Tile.TileInitializeData.IsOffset ? _colorData.ActiveOffsetColor : _colorData.ActiveDefaultColor;
                    break;
                default:
                    color = _colorData.InvisableDefaultColor;
                    break;
            }

            return color;
        }

        private Color GetHighlightColor()
        {
            bool isAvailable = false;

            if (Tile.TileInitializeData.TileType == TileType.Available && (Tile.PlacedItem == null || Tile.PlacedItem == Tile.LastInteractedItem))
            {
                isAvailable = true;
            }

            Color color = isAvailable ? _colorData.ActiveHighlightColor : _colorData.DeactiveHighlightColor;
            return color;
        }
    }
}
