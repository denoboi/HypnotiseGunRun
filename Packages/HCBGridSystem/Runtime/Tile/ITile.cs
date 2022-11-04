using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public interface ITile
    {
        Transform T { get; }
        TileInitializeData TileInitializeData { get; set; }
        //Vector2Int Grid { get; }
        bool IsOccupied { get; }
        bool IsAvailable { get; }
        string TileID { get; }
        IPlaceable PlacedItem { get; }
        void StartInteraction(IPlaceable placeable);
        void EndInteraction(IPlaceable placeable);
        void PlaceItem(IPlaceable placeable);
        void RemoveItem(IPlaceable placeable);
        void LoadTileData(TileSaveData tileSaveData);
    }
}
