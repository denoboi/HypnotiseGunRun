using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    public class GridObjectSaveData
    {
        public string GridObjectID;
        public string LastPlacedPivotTileID;

        public GridObjectSaveData(string gridObjectID, string lastPlacedPivotTileID) 
        {
            GridObjectID = gridObjectID;
            LastPlacedPivotTileID = lastPlacedPivotTileID;
        }
    }
}
