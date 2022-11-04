using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using HCB.SkinSystem;
using DG.Tweening;

namespace HCB.SkinSystem.Samples
{
    [System.Serializable]
    public class SkinItemVisuals
    {
        public SkinRarity SkinRarity;
        public Sprite BackgroundSprite;

        public void SetItemVisual(Image backgroundImage)
        {
            backgroundImage.sprite = BackgroundSprite;
        }
    }

    public class SkinItem : MonoBehaviour
    {
        [HideInInspector]
        public SkinItemData skinItemData;

        public List<SkinItemVisuals> SkinItemVisuals = new List<SkinItemVisuals>();

        public Image SkinIcon;
        public Image BackgroundImage;
        public Image Outline;

        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        private Tween scaleTween;

        private void OnEnable()
        {
            Button.onClick.AddListener(SelectSkin);
            if (Managers.Instance == null)
                return;

            SkinManager.Instance.OnNewSkinBought.AddListener(CheckAvailibility);
            SkinManager.Instance.OnSkinActivated.AddListener(CheckSelectedSkin);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(SelectSkin);
            if (Managers.Instance == null)
                return;

            SkinManager.Instance.OnSkinActivated.RemoveListener(CheckSelectedSkin);
        }

        public void Initialize(SkinItemData data)
        {
            DehighlitButton();
            skinItemData = data;
            Button.interactable = SkinManager.Instance.IsSkinUnlocked(skinItemData.SkinId);
            SkinIcon.sprite = skinItemData.SkinSprite;
            for (int i = 0; i < SkinItemVisuals.Count; i++)
            {
                if (skinItemData.SkinRarity == SkinItemVisuals[i].SkinRarity)
                    SkinItemVisuals[i].SetItemVisual(BackgroundImage);
            }

            CheckSelectedSkin(SkinManager.Instance.GetCurrentSkin(data.SkinType));
        }

        private void CheckSelectedSkin(SkinItemData data)
        {
            if (string.Equals(data.SkinId, skinItemData.SkinId))
            {
                HighlightItem();
            }
            else
            {
                DehighlitButton();
            }
        }

        public void SelectSkin()
        {
            SkinManager.Instance.EquipSkin(skinItemData);
            HighlightItem();
            EventManager.OnLogEvent.Invoke("SkinEvent", "SkinEquiped", skinItemData.SkinId);
        }

        public void CheckAvailibility()
        {
            Button.interactable = SkinManager.Instance.IsSkinUnlocked(skinItemData.SkinId);
        }

        

        public void HighlightItem()
        {
            if (scaleTween != null)
                scaleTween.Kill(true);

            transform.DOScale(Vector3.one * 1.2f, 0.2f);
            Outline.color = Color.white;
        }

        public void DehighlitButton()
        {
            if (scaleTween != null)
                scaleTween.Kill(true);

            transform.DOScale(Vector3.one, 0.2f);

            Outline.color = Color.clear;
        }

    }
}
