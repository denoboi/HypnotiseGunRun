using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.UI
{
    [RequireComponent(typeof(HCBPanel))]
    public abstract class UIPanelAnimationPanelBase : MonoBehaviour, IUIPanelAnimation
    {
        private HCBPanel hcbPanel;
        protected HCBPanel HCBPanel { get { return hcbPanel == null ? hcbPanel = GetComponent<HCBPanel>() : hcbPanel; } }

        [SerializeField] private float duration = 1;
        public float Duration { get => duration; set => duration = value; }

        [SerializeField] private Ease showEase = Ease.OutBack;
        public Ease ShowEase { get => showEase; set => showEase = value; }

        [SerializeField] private Ease hideEase = Ease.InBack;
        public Ease HideEase { get => hideEase; set => hideEase = value; }

        protected virtual void OnEnable()
        {
            HCBPanel.OnPanelShown.AddListener(DoShowAnimation);
            HCBPanel.OnPanelHide.AddListener(DoHideAnimation);
        }

        protected virtual void OnDisable()
        {
            HCBPanel.OnPanelShown.RemoveListener(DoShowAnimation);
            HCBPanel.OnPanelHide.RemoveListener(DoHideAnimation);
        }

        public abstract void DoHideAnimation();
        public abstract void DoShowAnimation();
    }
}
