using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.SkinSystem;
using HCB.UI;
using System;
using DG.Tweening;

namespace HCB.SkinSystem.Samples
{

    public class SkinCategoryButton : MonoBehaviour
    {
        public SkinType SkinType;

        protected SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }

        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        private Tween moveTween;

        private Vector3 startPos;
        [SerializeField] private float startOffset = 200;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            startPos = rectTransform.anchoredPosition;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(SetSkinPanel);
            SkinPanel.OnSkinPanelTypeChange.AddListener(SetSelection);
            SkinPanel.OnPanelShown.AddListener(ShowAnimation);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(SetSkinPanel);
            SkinPanel.OnSkinPanelTypeChange.RemoveListener(SetSelection);
            SkinPanel.OnPanelShown.RemoveListener(ShowAnimation);
        }

        private void SetSelection(SkinType skinType)
        {
            if (moveTween != null)
                moveTween.Kill();

            if (skinType == SkinType)
                moveTween = rectTransform.DOLocalMoveX(startPos.x - (startOffset / 10), 0.5f).SetEase(Ease.OutBack);
            else
                moveTween = rectTransform.DOLocalMoveX(startPos.x, 0.5f).SetEase(Ease.InBack);

        }

        private void SetSkinPanel()
        {
            SkinPanel.ChangeSkinType(SkinType);
        }

        private void ShowAnimation()
        {
            rectTransform.DOLocalMoveX(startPos.x + startOffset, 0.5f).SetEase(Ease.OutBack).OnComplete(() => SetSelection(SkinPanel.currentSkinType)).SetDelay((SkinPanel.currentSkinType == SkinType) ? 0.8f : 1f).From();
        }
    }
}
