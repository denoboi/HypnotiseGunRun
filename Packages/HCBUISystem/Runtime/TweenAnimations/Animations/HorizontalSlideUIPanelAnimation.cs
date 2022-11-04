using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.UI
{
    public class HorizontalSlideUIPanelAnimation : UIPanelAnimationPanelBase
    {

        private enum Direction { Left = -1, Right = 1}

        [SerializeField]
        private Direction direction = Direction.Left;

        public override void DoHideAnimation()
        {
            transform.DOLocalMoveX(transform.localPosition.x + ((Screen.width / 2) * (int)direction), Duration).SetEase(HideEase).OnComplete(() => HCBPanel.SetPanel(0, false, false));
        }

        public override void DoShowAnimation()
        {
            HCBPanel.SetPanel(1, true, true);
            transform.DOLocalMoveX(0, Duration).SetEase(ShowEase);
        }
    }
}
