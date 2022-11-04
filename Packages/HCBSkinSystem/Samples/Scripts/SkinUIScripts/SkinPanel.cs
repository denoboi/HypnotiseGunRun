using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HCB.Core;
using HCB.UI;
using System.Linq;

namespace HCB.SkinSystem.Samples
{
    public class SkinPanel : HCBPanel
    {
        public GameObject SkinItemPrefab;

        private SkinCategoryPanel skinCategoryPanel;
        protected SkinCategoryPanel SkinCategoryPanel { get { return (skinCategoryPanel == null) ? skinCategoryPanel = GetComponentInChildren<SkinCategoryPanel>() : skinCategoryPanel; } }

        [HideInInspector]
        public SkinTypeEvent OnSkinsInitialized = new SkinTypeEvent();

        [HideInInspector]
        public SkinTypeEvent OnSkinPanelTypeChange = new SkinTypeEvent();

        [HideInInspector]
        public SkinRarerityEvent OnSkinPanelRarityChanged = new SkinRarerityEvent();

        
        public SkinType currentSkinType = SkinType.Fullbody;
        public SkinRarity currentSkinRarity = SkinRarity.Common;

        private void Start()
        {
            InitializePanel();
        }

        private void OnEnable()
        {

            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
        }

        private void OnDisable()
        {

            if (Managers.Instance == null)
                return;

            LevelManager.Instance.OnLevelStart.AddListener(HidePanel);
        }

        public void ChangeSkinType(SkinType skinType)
        {
            currentSkinType = skinType;
            OnSkinPanelTypeChange.Invoke(currentSkinType);
            InitializePanel();
        }

        public void ChangeSkinRarerity(SkinRarity skinRarity)
        {
            currentSkinRarity = skinRarity;
            OnSkinPanelRarityChanged.Invoke(currentSkinRarity);
            InitializePanel();
        }

        private void InitializePanel()
        {
            List<SkinItemData> skinItems = SkinManager.Instance.GetSkinsByType(currentSkinType);
            SkinCategoryPanel.Initialize(SkinItemPrefab, new List<SkinItemData>(skinItems.Where(x => x.SkinRarity == currentSkinRarity)));
        }

        public void UnlockRandomSkin(SkinType skinType, SkinRarity skinRarity)
        {
            skinCategoryPanel.UnlockRandomAnimation(SkinManager.Instance.GetRandomSkin(skinType, skinRarity));
        }
    }

    public class SkinTypeEvent : UnityEvent<SkinType> { }
    public class SkinRarerityEvent : UnityEvent<SkinRarity> { }
}
