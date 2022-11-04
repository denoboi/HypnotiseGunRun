using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using HCB.UI;
using HCB.AchivementSystem;
using HCB.SkinSystem;
using Sirenix.OdinInspector;
using TMPro;
using System;

[System.Serializable]
public class RewardBackgroundColorDictionary : HCB.Utilities.UnitySerializedDictionary<SkinRarity, Color> { }

public class AchivementItem : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    private AchivementModel AchivementModel;

    public RewardBackgroundColorDictionary SkinRewardBackgroundCollection = new RewardBackgroundColorDictionary();

    [Header("Componenets")]
    public TextMeshProUGUI AchivementIDText;
    public TextMeshProUGUI AchivementDescriptionText;
    public TextMeshProUGUI AchivementProgressText;
    public TextMeshProUGUI AchivementCurrencyRewardText;
    public Image AchivementSkinRewardImage;
    public Image AchivementSkinRewardBG;
    public Slider AchivmentProgressSlider;

    [Header("RewardPanels")]
    public HCBPanel SkinRewardPanel;
    public HCBPanel CurrencyRewardPanel;


    private Button button;
    protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    private void OnEnable()
    {
        Button.onClick.AddListener(GiveReward);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(GiveReward);
    }

    public void Initialize(AchivementModel achivementModel)
    {
        AchivementModel = achivementModel;
        AchivementIDText.SetText(AchivementModel.AchivementID);
        AchivementDescriptionText.SetText(AchivementModel.AchivementDescription);
        AchivementCurrencyRewardText.SetText(AchivementModel.CurrencyRewardAmount.ToString());


        switch (AchivementModel.RewardType)
        {
            case AchivementRewardType.Currency:
                CurrencyRewardPanel.ShowPanel();
                SkinRewardPanel.HidePanel();
                break;
            case AchivementRewardType.Skin:
                CurrencyRewardPanel.HidePanel();
                SkinRewardPanel.ShowPanel();

                SkinItemData skinItemData = SkinManager.Instance.GetSkinByID(AchivementModel.SkinID);
                AchivementSkinRewardImage.sprite = skinItemData.SkinSprite;
                AchivementSkinRewardBG.color = SkinRewardBackgroundCollection[skinItemData.SkinRarity];
                break;
            default:
                CurrencyRewardPanel.ShowPanel();
                SkinRewardPanel.HidePanel();
                break;
        }

        int totalAchivementCount = 0;
        int currentAchivementIndex = 0;

        for (int i = 0; i < AchivementManager.Instance.PlayerAchivementData.Achivements.Count; i++)
        {
            if (string.Equals(AchivementModel.AchivementID, AchivementManager.Instance.PlayerAchivementData.Achivements[i].AchivementID))
            {
                totalAchivementCount++;
                if (AchivementModel.UnlockTreashold == AchivementManager.Instance.PlayerAchivementData.Achivements[i].UnlockTreashold)
                    currentAchivementIndex = totalAchivementCount;
            }
        }

        AchivmentProgressSlider.maxValue = AchivementModel.UnlockTreashold;
        AchivmentProgressSlider.value = AchivementModel.currentValue;
        AchivementProgressText.SetText(currentAchivementIndex + "/" + totalAchivementCount);
        Button.interactable = AchivementModel.IsComplete;
    }

    private void GiveReward()
    {
        switch (AchivementModel.RewardType)
        {
            case AchivementRewardType.Currency:
                GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += AchivementModel.CurrencyRewardAmount;
                EventManager.OnPlayerDataChange.Invoke();
                break;
            case AchivementRewardType.Skin:
                SkinItemData skinItemData = SkinManager.Instance.GetSkinByID(AchivementModel.SkinID);
                SkinManager.Instance.BuySkin(skinItemData);
                break;
            default:
                break;
        }

        AchivementModel.IsCollected = true;
        Button.interactable = false;
        AchivementModel achivementModel = AchivementManager.Instance.GetAchivementNotCollected(AchivementModel.AchivementID);
        if(achivementModel !=null)
            Initialize(achivementModel);

    }
}
