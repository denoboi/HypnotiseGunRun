using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.UI;
using UnityEngine.UI;
using System;

public class CloseButton : MonoBehaviour
{
    [ValueDropdown("panelList")]
    public string ConnecedPanel;

    private Button button;
    protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    private List<string> panelList
    {
        get { return HCBPanelList.PanelIDs; }
    }


    private void OnEnable()
    {
        Button.onClick.AddListener(ClosePanel);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(ClosePanel);
    }

    private void ClosePanel()
    {
        if (string.IsNullOrEmpty(ConnecedPanel))
            return;

        HCBPanelList.HCBPanels[ConnecedPanel].HidePanel();
    }
}
