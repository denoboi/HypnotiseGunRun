using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.UI
{
    public interface IUIPanelAnimation 
    {
        public float Duration { get; set; }
        public Ease ShowEase { get; set; }
        public Ease HideEase { get; set; }

        void DoShowAnimation();
        void DoHideAnimation();
    }
}
