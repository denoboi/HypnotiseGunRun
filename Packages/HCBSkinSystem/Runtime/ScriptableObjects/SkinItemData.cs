using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace HCB.SkinSystem
{
    public enum SkinType
    {
        Fullbody,
    }

    public enum SkinRarity
    {
        Common,
        Rare
    }

    public class SkinItemData : ScriptableObject
    {
        public string SkinId;
        public SkinType SkinType;
        public SkinRarity SkinRarity;
        public GameObject SkinPrefab;
        public Sprite SkinSprite;

#if UNITY_EDITOR
        [Button]
        private void SetUpData(string _allise)
        {
            string allise = (string.IsNullOrEmpty(_allise)) ? SkinPrefab.name : _allise;

            string newName = "Skin_" + SkinType + "_" + SkinRarity + "_" + "Data" + "_" + allise;

            string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();

            SkinId  = "Skin_" + SkinType  + "_" + allise;
            UnityEditor.AssetDatabase.Refresh();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
