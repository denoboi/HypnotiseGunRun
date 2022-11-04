using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.UI;
using HCB.Core;
using HCB.Utilities;

namespace HCB.SkinSystem.Samples
{
    [System.Serializable]
    public class UnlockSkinVisual
    {
        public SkinRarity skinRarity;
        public Sprite BackgroundSprite;
        public bool isRewarded;

        public void SetVisual(Image backgroundImage, GameObject rewardedPanel, GameObject pricePanel)
        {
            backgroundImage.sprite = BackgroundSprite;
            if (isRewarded)
            {
                rewardedPanel.SetActive(true);
                pricePanel.SetActive(false);
            }
            else
            {
                rewardedPanel.SetActive(false);
                pricePanel.SetActive(true);
            }
        }
    }

    public class UnlockSkinButton : HCBPanel
    {
        public List<UnlockSkinVisual> unlockSkinVisuals;

        public Image BackgroundImage;
        public GameObject RewardedPanel;
        public GameObject PricePanel;

        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        protected SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }

        private SkinType skinType;
        private SkinRarity skinRarity;

        private float ItemUnlockCost { get { return PlayerPrefs.GetFloat(PlayerPrefKeys.ItemUnlockCost, 1000); } }


       
        private void Start()
        {
            SkinPanel.OnPanelShown.AddListener(SetButton);
            SkinPanel.OnSkinPanelRarityChanged.AddListener(ChangeButtonRarerity);
            SkinPanel.OnSkinPanelTypeChange.AddListener(ChangeButtonSkinType);
            Button.onClick.AddListener(UnlockSkin);

          
        }

        

        private void OnDisable()
        {
            SkinPanel.OnPanelShown.RemoveListener(SetButton);
            SkinPanel.OnSkinPanelRarityChanged.RemoveListener(ChangeButtonRarerity);
            SkinPanel.OnSkinPanelTypeChange.RemoveListener(ChangeButtonSkinType);
            Button.onClick.RemoveListener(UnlockSkin);
        }


        private void SetButton()
        {
            CheckButtonAvailability();
            for (int i = 0; i < unlockSkinVisuals.Count; i++)
            {
                if (unlockSkinVisuals[i].skinRarity == skinRarity)
                {
                    unlockSkinVisuals[i].SetVisual(BackgroundImage, RewardedPanel, PricePanel);
                    break;
                }
            }
        }

        private void ChangeButtonSkinType(SkinType _skinType)
        {
            skinType = _skinType;
        }


        private void ChangeButtonRarerity(SkinRarity _skinRarity)
        {
            skinRarity = _skinRarity;
            SetButton();
        }

        private void UnlockSkin()
        {
            switch (skinRarity)
            {
                case SkinRarity.Common:
                    UnlockWithCurrency();
                    break;
                case SkinRarity.Rare:
                    UnlockWithRewarded();
                    break;
                default:
                    break;
            }
        }

        private void CheckButtonAvailability()
        {
            if (!SkinManager.Instance.CheckSkinAvialibity(skinType, skinRarity))
            {
                Button.interactable = false;
                HidePanel();
                return;
            }else
            {
                ShowPanel();
            }

            switch (skinRarity)
            {
                case SkinRarity.Common:
                    Button.interactable = GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] >= ItemUnlockCost;
                    break;
                case SkinRarity.Rare:
                    //Button.interactable = AdManager.Instance.IsRewardedAvailable;
                    break;
                default:
                    break;
            }
        }

        private void UnlockWithRewarded()
        {
            //AdManager.Instance.ShowRewarded(() => {
            //    SkinPanel.UnlockRandomSkin(skinType, skinRarity);
            //    CheckButtonAvailability();
            //    AnalitycsManager.Instance.LogEvent("SkinEvent", "SkinUnlock", "UnlockWithRewarded");
            //    AnalitycsManager.Instance.LogEvent("RewardedSuccessEvent", "SkinUnlock", "UnlockWithRewarded");
            //});
        }

        private void UnlockWithCurrency()
        {
            if (GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] <= ItemUnlockCost)
                return;

            SkinPanel.UnlockRandomSkin(skinType, skinRarity);
            GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] -= ItemUnlockCost;
            CheckButtonAvailability();
            EventManager.OnLogEvent.Invoke("SkinEvent", "SkinUnlock", "CashBuy");
        }
    }
}
