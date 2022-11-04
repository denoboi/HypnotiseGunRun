using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    [System.Serializable]
    public class TileSaveData
    {
        public bool IsPurchased;
        public string TileID;

        public TileSaveData(string tileID)
        {
            TileID = tileID;
        }
    }
}
