using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.SkinSystem;
using HCB.SkinSystem.Samples;
using HCB.UI;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using DG.Tweening;

[System.Serializable]
public class BackgroundVisials
{
    public SkinRarity SkinRarity;
    public Sprite BackgroundSprite;

    public void ChangeBackgroundVisial(Image backgroundImage)
    {
        backgroundImage.sprite = BackgroundSprite;
    }
}

public class SkinPanelBackgroundController : SerializedMonoBehaviour
{
    public Image BackgroundImage;

    private SkinPanel SkinPanel { get { return HCBPanelList.HCBPanels[HCBPanelList.SkinPanel] as SkinPanel; } }

    public List<BackgroundVisials> BackgroundVisials = new List<BackgroundVisials>();


    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void OnEnable()
    {
        SkinPanel.OnSkinPanelRarityChanged.AddListener(ChangeBackground);
        SkinPanel.OnPanelShown.AddListener(ShowAnimation);
    }

    private void OnDisable()
    {
        SkinPanel.OnSkinPanelRarityChanged.RemoveListener(ChangeBackground);
        SkinPanel.OnPanelShown.RemoveListener(ShowAnimation);
    }

    private void ChangeBackground(SkinRarity skinRarity)
    {
        BackgroundVisials.Where(x => x.SkinRarity == skinRarity).First().ChangeBackgroundVisial(BackgroundImage);
    }

    private void ShowAnimation()
    {
        transform.DOMoveY(startPos.x - 400, 0.5f).SetEase(Ease.OutBack).From();
    }
}
