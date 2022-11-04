using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Linq;

namespace HCB.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HCBPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        protected CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }

        [ValueDropdown("panelList")]
        public string PanelID;

        public UnityEvent OnPanelShown = new UnityEvent();
        public UnityEvent OnPanelHide = new UnityEvent();



        private List<string> panelList
        {
            get { return HCBPanelList.PanelIDs; }
        }

        protected virtual void Awake()
        {
            HCBPanelList.HCBPanels[PanelID] = this;
        }


        [ButtonGroup("PanelVisibility")]
        public virtual void ShowPanel()
        {
            if (CanvasGroup.alpha > 0)
                return;

            IUIPanelAnimation panelAnimation = GetComponent<IUIPanelAnimation>();

            if (panelAnimation != null)
                panelAnimation.DoShowAnimation();
            else SetPanel(1, true,true);
        }

        [ButtonGroup("PanelVisibility")]
        public virtual void HidePanel()
        {
            if (CanvasGroup.alpha == 0)
                return;

            IUIPanelAnimation panelAnimation = GetComponent<IUIPanelAnimation>();

            if (panelAnimation != null)
                panelAnimation.DoHideAnimation();
            else SetPanel(0, false, false);
        }

        public void SetPanel(float alpha, bool interactable, bool blocksRaycast)
        {
            CanvasGroup.alpha = alpha;
            CanvasGroup.interactable = interactable;
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }

        [ButtonGroup("PanelVisibility")]
        public virtual void TogglePanel()
        {
            if (CanvasGroup.alpha == 0)
                ShowPanel();
            else HidePanel();
        }

#if UNITY_EDITOR

#endif

    }
}
