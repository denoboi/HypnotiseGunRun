using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCB.GridSystem
{
    [System.Serializable]
    public class GridSystemData
    {
        public GridSystemData() 
        {
            TileSaveDatas = new List<TileSaveData>();
            GridObjectSaveDatas = new List<GridObjectSaveData>();
        }

        private List<TileSaveData> _tileSaveDatas = new List<TileSaveData>();
        public List<TileSaveData> TileSaveDatas { get => _tileSaveDatas; set => _tileSaveDatas = value; }

        private List<GridObjectSaveData> _gridObjectSaveDatas = new List<GridObjectSaveData>();
        public List<GridObjectSaveData> GridObjectSaveDatas { get => _gridObjectSaveDatas; set => _gridObjectSaveDatas = value; }
    }
}
