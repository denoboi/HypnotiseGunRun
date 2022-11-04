using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;

#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace HCB.GridSystem.Editor
{
#if UNITY_EDITOR
    public class GridSystemDataEditor : OdinEditorWindow
    {
        public GridSystemData GridSystemData;

        [MenuItem("HyperCasualBase/Data/Grid System Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<GridSystemDataEditor>().Show();

        }
        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            GridSystemData = SaveLoadManager.LoadPDP(GridSystemManager.SAVE_FILE_NAME, new GridSystemData());
        }


        [Button]
        public void SaveData()
        {
            SaveLoadManager.SavePDP(GridSystemData, GridSystemManager.SAVE_FILE_NAME);
        }

        [Button]
        public void DeleteData()
        {
            SaveLoadManager.DeleteFile(GridSystemManager.SAVE_FILE_NAME);
            PlayerPrefs.DeleteAll();
            GridSystemData = new GridSystemData();
        }
    }
#endif
}
