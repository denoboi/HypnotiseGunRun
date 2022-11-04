using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using HCB.Core;

namespace HCB.SkinSystem.Editor
{

    public class SkinDataEditor : OdinEditorWindow
    {
        public PlayerSkinData PlayerSkinData;

        [MenuItem("HyperCasualBase/Data/Skin Data Editor")]
        private static void OpenWindow()
        {
            GetWindow<SkinDataEditor>().Show();

        }
        [Button]
        protected override void Initialize()
        {
            base.Initialize();
            PlayerSkinData = SaveLoadManager.LoadPDP(SavedFileNameHolder.SkinData, new PlayerSkinData());
        }


        [Button]
        public void SaveData()
        {
            SaveLoadManager.SavePDP(PlayerSkinData, SavedFileNameHolder.SkinData);
        }

        [Button]
        public void DeleteData()
        {
            SaveLoadManager.DeleteFile(SavedFileNameHolder.SkinData);
            PlayerSkinData = new PlayerSkinData();
        }
    }
}