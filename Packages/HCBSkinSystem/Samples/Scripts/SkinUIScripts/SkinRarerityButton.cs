using System.Collections;
using System.Collections.Generic;
using HCB.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace HCB.SkinSystem.Samples
{
    public class SkinRarerityButton : MonoBehaviour
    {
        public SkinRarity SkinRarity;

        protected SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }

        private Button button;
        protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

        private Vector3 startPos;

        Tween moveTween;

        private void Start()
        {
            startPos = transform.localPosition;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(SetSkinPanel);
            SkinPanel.OnSkinPanelRarityChanged.AddListener(SetSelection);
            SkinPanel.OnPanelShown.AddListener(ShowAnimation);
        }

        private void OnDisable()
        {
            Button.onClick.AddListener(SetSkinPanel);
            SkinPanel.OnSkinPanelRarityChanged.RemoveListener(SetSelection);
            SkinPanel.OnPanelShown.RemoveListener(ShowAnimation);
        }

        private void SetSkinPanel()
        {
            SkinPanel.ChangeSkinRarerity(SkinRarity);
        }

        private void SetSelection(SkinRarity skinRarerity)
        {
            if (moveTween != null)
                moveTween.Kill();

            if (SkinRarity == skinRarerity)
                moveTween = transform.DOLocalMoveY(startPos.y + 10, 0.5f).SetEase(Ease.OutBack);
            else
                moveTween = transform.DOLocalMoveY(startPos.y, 0.5f).SetEase(Ease.InBack);

        }

        private void ShowAnimation()
        {
            transform.DOLocalMoveY(startPos.y - 100, 0.5f).SetEase(Ease.OutBack).OnComplete(() => SetSelection(SkinPanel.currentSkinRarity)).SetDelay((SkinPanel.currentSkinRarity == SkinRarity) ? 0.8f : 1f).From();
        }
    }
}