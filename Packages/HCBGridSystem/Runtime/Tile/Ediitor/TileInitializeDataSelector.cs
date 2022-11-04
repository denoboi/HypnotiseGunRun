using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using UnityEngine;

namespace HCB.GridSystem.Editor
{
#if UNITY_EDITOR
    public class TileInitializeDataSelector : OdinEditorWindow
    {
        public TileInitializeData TileInitializeData;
        private TileCreateData tileCreateData;

        public void Initialize(TileInitializeData _tileInitializeData, TileCreateData _tileCreateData)
        {
            TileInitializeData = _tileInitializeData;
            tileCreateData = _tileCreateData;
        }


        [Button]
        private void SaveTileChanges()
        {
            tileCreateData.SaveTileData(TileInitializeData);
            GetWindow<TileInitializeDataSelector>().Close();
        }        
    }
#endif
}
