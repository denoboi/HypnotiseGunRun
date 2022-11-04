using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif
using HCB.OfflineIncomeSystem;
using HCB.Core;

namespace HCB.OfflineIncomeSystemEditor
{
#if UNITY_EDITOR
    public class OfflineIncomeDataEditor : OdinEditorWindow
    {
        public OfflineIncomeData OfflineIncomeData;

        [MenuItem("HyperCasualBase/Data/Offline Income Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<OfflineIncomeDataEditor>().Show();

        }
        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            OfflineIncomeData = SaveLoadManager.LoadPDP(OfflineIncomeManager.SAVE_FILE_NAME, new OfflineIncomeData());
        }


        [Button]
        public void SaveData()
        {
            SaveLoadManager.SavePDP(OfflineIncomeData, OfflineIncomeManager.SAVE_FILE_NAME);
        }

        [Button]
        public void DeleteData()
        {
            SaveLoadManager.DeleteFile(OfflineIncomeManager.SAVE_FILE_NAME);
            PlayerPrefs.DeleteAll();
            OfflineIncomeData = new OfflineIncomeData();
        }
    }
#endif
}
