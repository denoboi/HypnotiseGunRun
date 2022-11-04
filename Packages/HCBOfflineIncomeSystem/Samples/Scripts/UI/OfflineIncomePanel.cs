using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HCB.OfflineIncomeSystem;
using HCB.Utilities;
using HCB.Core;

namespace HCB.OfflineIncomeSystemSamples
{
    public class OfflineIncomePanel : FadePanelBase
    {
        public TextMeshProUGUI IncomeTextMesh;        

        private void Start()
        {
            CheckOfflineIncome();
        }

        //Button callback
        public void Collect() 
        {
            HidePanelAnimated();

            GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += OfflineIncomeManager.Instance.OfflineIncome.Value;
            EventManager.OnPlayerDataChange.Invoke();
        }

        private void CheckOfflineIncome() 
        {
            if (OfflineIncomeManager.Instance.OfflineIncome == null)
                return;

            SetIncomeText();
            ShowPanelAnimated();
        }

        private void SetIncomeText() 
        {
            IncomeTextMesh.SetText(HCBUtilities.ScoreShowF2(OfflineIncomeManager.Instance.OfflineIncome.Value));
        }
    }
}
