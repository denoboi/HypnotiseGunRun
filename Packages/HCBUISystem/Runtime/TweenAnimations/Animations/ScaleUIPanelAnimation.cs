using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.UI
{
    public class ScaleUIPanelAnimation : UIPanelAnimationPanelBase
    {

        public override void DoShowAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, Duration).SetEase(ShowEase).From();
            HCBPanel.SetPanel(1, true, true);
        }

        public override void DoHideAnimation()
        {
            transform.localScale = Vector3.one;
            transform.DOScale(Vector3.zero, Duration).SetEase(HideEase).OnComplete(() => HCBPanel.SetPanel(0, false, false));
        }
    }
}
