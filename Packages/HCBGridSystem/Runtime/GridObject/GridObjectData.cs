using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Utilities;

namespace HCB.GridSystem
{
    public class GridObjectData : ScriptableObject
    {
        public string Name;        
        public GameObject Prefab;
        [InfoBox("Set ID", InfoMessageType.Error, VisibleIf = "IsEmpty")]
        [ReadOnly] public string ID;

        [Button]
        private void SetID() 
        {
            int IDLength = 10;
            ID = HCBUtilities.RandomString(IDLength);
        }        

        private bool IsEmpty() 
        {
            return string.IsNullOrEmpty(ID);
        }
    }
}
