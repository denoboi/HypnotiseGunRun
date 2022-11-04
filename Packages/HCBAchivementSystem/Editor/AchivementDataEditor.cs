using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using HCB.Core;
using HCB.Utilities;

namespace HCB.AchivementSystem.Editor
{
    public class AchivementDataEditor : OdinEditorWindow
    {
        public PlayerAchivementData PlayerAchivementData;

        [MenuItem("HyperCasualBase/Data/Achivement Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<AchivementDataEditor>().Show();

        }
        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            PlayerAchivementData = SaveLoadManager.LoadPDP(SavedFileNameHolder.AchivementData, new PlayerAchivementData());
        }


        [Button]
        public void SaveData()
        {
            SaveLoadManager.SavePDP(PlayerAchivementData, SavedFileNameHolder.AchivementData);
        }

        [Button]
        public void DeleteData()
        {
            SaveLoadManager.DeleteFile(SavedFileNameHolder.AchivementData);
            PlayerAchivementData = new PlayerAchivementData();
        }
    }
}
