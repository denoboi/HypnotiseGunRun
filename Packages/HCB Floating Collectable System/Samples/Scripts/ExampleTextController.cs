using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HCB.Core;

namespace HCB.FloatingCollectableSystem.Examples
{
    public class ExampleTextController : MonoBehaviour
    {
        TextMeshProUGUI _textMesh;
        TextMeshProUGUI TextMesh => _textMesh == null ? _textMesh = GetComponent<TextMeshProUGUI>() : _textMesh;

        private float _currentCollectedAmount = 0f;

        private void OnEnable()
        {
            EventManager.OnCurrencyInteracted.AddListener(UpdateText);
        }

        private void OnDisable()
        {
            EventManager.OnCurrencyInteracted.RemoveListener(UpdateText);
        }

        private void UpdateText(ExchangeType exchangeType, int amount)
        {
            if (exchangeType == ExchangeType.Coin)
            {
                _currentCollectedAmount++;
            }

            TextMesh.text = _currentCollectedAmount.ToString();
        }
    }
}
