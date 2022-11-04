using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;

namespace HCB.GridSystem.Samples
{
    public class AddBalanceButton : MonoBehaviour
    {
        private Button _button;
        private Button Button => _button == null ? _button = GetComponentInChildren<Button>() : _button;

        private const float INCREASE_AMOUNT = 500f;        

        private void OnEnable()
        {
            Button.onClick.AddListener(AddBalance);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(AddBalance);
        }

        private void AddBalance()
        {
            GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += INCREASE_AMOUNT;
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}
