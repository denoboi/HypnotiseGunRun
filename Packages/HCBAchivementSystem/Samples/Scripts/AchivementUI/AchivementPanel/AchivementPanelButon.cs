using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using HCB.UI;
using System;
using DG.Tweening;
using HCB.Utilities;
using HCB.SkinSystem.Samples;
using HCB.AchivementSystem;

public class AchivementPanelButon : HCBPanel
{

    private Button button;
    protected Button Button { get { return (button == null) ? button = GetComponent<Button>() : button; } }

    protected SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }
    protected AchivementPanel AchivementPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.AchivementPanel] as AchivementPanel; } }

    private RectTransform rectTransform;
    protected RectTransform RectTransform { get { return (rectTransform == null) ? rectTransform = GetComponent<RectTransform>() : rectTransform; } }

    public Image NotificationImage;

    private Vector3 startpos;

    private void Awake()
    {
        startpos = RectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        Button.onClick.AddListener(OpenAchivementPanel);
        SceneController.Instance.OnSceneLoaded.AddListener(ShowPanel);
        SkinPanel.OnPanelHide.AddListener(ShowPanel);
        SkinPanel.OnPanelShown.AddListener(HidePanel);
        AchivementManager.Instance.OnAchivementUnlock.AddListener(ShowNotificaion);
        //AchivementPanel.OnPanelHide.AddListener(ShowPanel);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OpenAchivementPanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowPanel);
        SkinPanel.OnPanelHide.RemoveListener(ShowPanel);
        SkinPanel.OnPanelShown.RemoveListener(HidePanel);
        AchivementManager.Instance.OnAchivementUnlock.RemoveListener(ShowNotificaion);
        //AchivementPanel.OnPanelHide.RemoveListener(ShowPanel);
    }

    private void ShowNotificaion(AchivementModel achivementModel)
    {
        NotificationImage.enabled = true;
    }

    private void OpenAchivementPanel()
    {
        AchivementPanel.ShowPanel();
        NotificationImage.enabled = false;
        //HidePanel();
        EventManager.OnLogEvent.Invoke("SkinEvent", "SkinPanelOpen", "Level " + PlayerPrefs.GetInt(PlayerPrefKeys.FakeLevel, 0).ToString());
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        RectTransform.DOLocalMoveX(startpos.x, 1f).SetEase(Ease.InBack);
    }

    public override void HidePanel()
    {
        transform.DOLocalMoveX(startpos.x - 500, 0.6f).SetEase(Ease.OutBack).OnComplete(base.HidePanel);
    }
}
