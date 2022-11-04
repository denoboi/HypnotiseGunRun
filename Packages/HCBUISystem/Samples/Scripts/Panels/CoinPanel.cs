using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using DG.Tweening;
using TMPro;
using HCB.Utilities;

namespace HCB.UI
{
    public class CoinPanel : HCBPanel
    {
        public TextMeshProUGUI BalanceTextMesh;        

        private const float BALANCE_TWEEN_DURATION = 0.25f;

        private string _balanceTweenID;
        private double _currentBalance;      

        private void Start()
        {
            SetDefaultValues();
        }

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

            EventManager.OnPlayerDataChange.AddListener(UpdateBalance);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            EventManager.OnPlayerDataChange.RemoveListener(UpdateBalance);           
        }

        private void UpdateBalance()
        {
            double targetAmount = GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin];
           
            DOTween.Kill(_balanceTweenID);
            DOTween.To(() => _currentBalance, x => _currentBalance = x, targetAmount, BALANCE_TWEEN_DURATION).SetEase(Ease.Linear).SetId(_balanceTweenID).OnUpdate(() =>
            {
                SetBalanceText(_currentBalance);
            });
        }        

        private void SetDefaultValues() 
        {
            _balanceTweenID = GetInstanceID() + "BalanceTweenID";
            _currentBalance = GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin];
            SetBalanceText(_currentBalance);            
        }

        private void SetBalanceText(double balance)
        {
            BalanceTextMesh.SetText(HCBUtilities.ScoreShowF2(balance));
        }
    }
}
