using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace HCB.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HCBPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        protected CanvasGroup CanvasGroup { get { return (canvasGroup == null) ? canvasGroup = GetComponent<CanvasGroup>() : canvasGroup; } }

        [ButtonGroup("PanelVisibility")]
        public virtual void ShowPanel()
        {
            CanvasGroup.alpha = 1;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        [ButtonGroup("PanelVisibility")]
        public virtual void HidePanel()
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
    }
}
