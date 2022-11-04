using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class GridObjectDataManager : Singleton<GridObjectDataManager>
    {
        private Dictionary<string, GridObjectData> _gridObjectDatasByID = new Dictionary<string, GridObjectData>();
        public Dictionary<string, GridObjectData> GridObjectDatasByID { get => _gridObjectDatasByID; private set => _gridObjectDatasByID = value; }        

        [SerializeField] private List<GridObjectData> _gridObjectDatas = new List<GridObjectData>();
        public List<GridObjectData> GridObjectDatas { get => _gridObjectDatas; private set => _gridObjectDatas = value; }

        private void Awake()
        {         
            SetProductDataDictCollection();
        }

        private void SetProductDataDictCollection()
        {
            foreach (var gridObjectData in GridObjectDatas)
            {
                if (!GridObjectDatasByID.ContainsKey(gridObjectData.ID))
                {
                    GridObjectDatasByID.Add(gridObjectData.ID, gridObjectData);
                }
            }           
        }        
    }
}

