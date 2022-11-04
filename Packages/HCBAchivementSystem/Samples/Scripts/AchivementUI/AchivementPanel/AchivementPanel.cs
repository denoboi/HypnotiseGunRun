using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using HCB.UI;
using HCB.AchivementSystem;

public class AchivementPanel : HCBPanel
{
    public GameObject AchivementItemPrefab;
    public Transform Content;

    private List<GameObject> createdItems = new List<GameObject>();

    public override void ShowPanel()
    {
        Initialize();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
        ClearItems();
    }

    public void Initialize()
    {
        List<AchivementModel> achivementModels = new List<AchivementModel>(AchivementManager.Instance.PlayerAchivementData.Achivements);
        string lastAchivementID = string.Empty;
        for (int i = 0; i < achivementModels.Count; i++)
        {
            if (achivementModels[i].IsCollected)
                continue;

            if (string.Equals(lastAchivementID, achivementModels[i].AchivementID))
                continue;

            lastAchivementID = achivementModels[i].AchivementID;
            GameObject achivementItemObject = Instantiate(AchivementItemPrefab, Content);
            AchivementItem achivementItem = achivementItemObject.GetComponent<AchivementItem>();
            achivementItem.Initialize(achivementModels[i]);
            createdItems.Add(achivementItemObject);

        }
    }

    public void ClearItems()
    {
        for (int i = 0; i < createdItems.Count; i++)
        {
            Destroy(createdItems[i]);
        }

        createdItems.Clear();
    }
}
