using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HCB.Core;
using HCB.AchivementSystem;
using HCB.SkinSystem;
using HCB.UI;
using DG.Tweening;
using System.Threading.Tasks;

namespace HCB.AchivementSystem.Samples
{
    public class AchivementNotification : HCBPanel
    {
        [Header("Components")]
        public TextMeshProUGUI AchivementIDText;
        public TextMeshProUGUI AchivementDescriptionText;
        public HCBPanel SkinRewardPanel;
        public Image SkinRewardIconImage;
        public HCBPanel CurrencyRewardPanel;
        public TextMeshProUGUI CurrencyRewardText;


        private RectTransform rectTransform;
        protected RectTransform RectTransform { get { return (rectTransform == null) ? rectTransform = GetComponent<RectTransform>() : rectTransform; } }

        private Vector3 startPos;

        private List<AchivementModel> achivementModels = new List<AchivementModel>();


        private void Start()
        {
            startPos = RectTransform.anchoredPosition;
        }


        private void OnEnable()
        {
            AchivementManager.Instance.OnAchivementUnlock.AddListener(ShowNotification);
        }

        private void OnDisable()
        {
            AchivementManager.Instance.OnAchivementUnlock.RemoveListener(ShowNotification);
        }

        private bool isNotificationShowing;

        private List<AchivementModel> currentAchivements = new List<AchivementModel>();

        public async void ShowNotification(AchivementModel achivementModel)
        {
            while (isNotificationShowing)
            {
                await Task.Yield();
            }

            isNotificationShowing = true;
            SetUpNotification(achivementModel);
            ShowPanel();
        }

        private void SetUpNotification(AchivementModel achivementModel)
        {
            switch (achivementModel.RewardType)
            {
                case AchivementRewardType.Currency:
                    SkinRewardPanel.HidePanel();
                    CurrencyRewardPanel.ShowPanel();
                    CurrencyRewardText.SetText(achivementModel.CurrencyRewardAmount.ToString());
                    break;
                case AchivementRewardType.Skin:
                    SkinRewardPanel.ShowPanel();
                    CurrencyRewardPanel.HidePanel();
                    SkinRewardIconImage.sprite = SkinManager.Instance.GetSkinByID(achivementModel.SkinID).SkinSprite;
                    break;
                default:
                    break;
            }
            AchivementIDText.SetText(achivementModel.AchivementID);
            AchivementDescriptionText.SetText(achivementModel.AchivementDescription);
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            RectTransform.DOLocalMoveX(startPos.x + RectTransform.rect.width, 0.6f).SetEase(Ease.OutBack);
            Run.After(4f, HidePanel);
        }

        public override void HidePanel()
        {
            RectTransform.DOLocalMoveX(startPos.x, 0.6f).SetEase(Ease.OutBack).OnComplete(()=>
            {
                base.HidePanel();
                Run.After(1f, () => isNotificationShowing = false);
            });
        }
    }
}
