using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.UI
{
    public class VerticalSlidePanelAnimation : UIPanelAnimationPanelBase
    {
        private enum Direction { Up = 1, Down = -1 }

        [SerializeField]
        private Direction direction = Direction.Up;

        public override void DoHideAnimation()
        {
            transform.DOLocalMoveY(transform.localPosition.y + ((Screen.width) * (int)direction), Duration).SetEase(HideEase).OnComplete(() => HCBPanel.SetPanel(0, false, false));
        }

        public override void DoShowAnimation()
        {
            HCBPanel.SetPanel(1, true, true);
            transform.DOLocalMoveY(0, Duration).SetEase(ShowEase);
        }
    }
}
