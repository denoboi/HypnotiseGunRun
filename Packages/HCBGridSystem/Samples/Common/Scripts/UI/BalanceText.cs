using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.Core;
using TMPro;
using HCB.Utilities;

namespace HCB.GridSystem.Samples
{
    public class BalanceText : MonoBehaviour
    {
        private TextMeshProUGUI _textMesh;
        private TextMeshProUGUI TextMesh => _textMesh == null ? _textMesh = GetComponentInChildren<TextMeshProUGUI>() : _textMesh;

        private const string DOLAR_SIGN = "$";

        private void Awake()
        {
            SetText();
        }

        private void OnEnable()
        {
            EventManager.OnPlayerDataChange.AddListener(SetText);
        }

        private void OnDisable()
        {
            EventManager.OnPlayerDataChange.RemoveListener(SetText);
        }

        private void SetText() 
        {
            string text = DOLAR_SIGN + HCBUtilities.ScoreShow(GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin]);
            TextMesh.SetText(text);
        }
    }
}
