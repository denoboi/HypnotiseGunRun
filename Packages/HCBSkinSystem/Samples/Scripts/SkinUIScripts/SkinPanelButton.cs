using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using HCB.UI;
using System;
using DG.Tweening;
using HCB.Utilities;

namespace HCB.SkinSystem.Samples
{
    public class SkinPanelButton : HCBPanel
    {
        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        protected SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }
        private bool OnboardingComplete { get { return (PlayerPrefs.GetInt("OnboardingComplete", 0) == 1) ? true : false; } }

        private RectTransform rectTransform;
        protected RectTransform RectTransform { get { return (rectTransform == null) ? rectTransform = GetComponent<RectTransform>() : rectTransform; } }

        private Vector3 startpos;

        private void Awake()
        {
            startpos = RectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            Button.onClick.AddListener(OpenSkinPanel);
            SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
            SkinPanel.OnPanelHide.AddListener(ShowPanel);
            SkinPanel.OnPanelShown.AddListener(HidePanel);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            Button.onClick.RemoveListener(OpenSkinPanel);
            SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
            SkinPanel.OnPanelHide.RemoveListener(ShowPanel);
            SkinPanel.OnPanelShown.RemoveListener(HidePanel);
        }

        private void OpenSkinPanel()
        {
            SkinPanel.ShowPanel();
            HidePanel();
            EventManager.OnLogEvent.Invoke("SkinEvent", "SkinPanelOpen", "Level " + PlayerPrefs.GetInt(PlayerPrefKeys.FakeLevel, 0).ToString());
        }

        public override void ShowPanel()
        {
            base.ShowPanel();
            if (!OnboardingComplete)
                HidePanel();

            RectTransform.DOLocalMoveX(startpos.x, 1f).SetEase(Ease.InBack);
        }

        public override void HidePanel()
        {
            transform.DOLocalMoveX(startpos.x + 500, 0.6f).SetEase(Ease.OutBack).OnComplete(base.HidePanel);
        }

    }
}
