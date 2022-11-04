using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.UI;
using DG.Tweening;
using System;


namespace HCB.OfflineIncomeSystemSamples
{
    public abstract class FadePanelBase : HCBPanel
    {
        protected const float FADE_IN_DURATION = 0.5f;
        protected const float FADE_OUT_DURATION = 0.5f;        

        protected const float MAX_FADE = 1f;
        protected const float MIN_FADE = 0f;

        protected string _fadeTweenID;
        
        protected override void Awake()
        {
            base.Awake();
            _fadeTweenID = GetInstanceID() + "FadeTweenID";
        }

        protected virtual void ShowPanelAnimated()
        {
            FadeTween(MAX_FADE, FADE_IN_DURATION, ShowPanel);
        }

        protected virtual void HidePanelAnimated()
        {
            FadeTween(MIN_FADE, FADE_OUT_DURATION, HidePanel);
        }

        protected void FadeTween(float endValue, float duration, Action onComplete = null)
        {
            DOTween.Kill(_fadeTweenID);
            CanvasGroup.DOFade(endValue, duration).SetId(_fadeTweenID).SetEase(Ease.Linear).OnComplete(() =>
            {
                onComplete?.Invoke();
            });
        }
    }
}
