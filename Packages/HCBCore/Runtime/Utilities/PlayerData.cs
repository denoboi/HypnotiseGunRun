using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using HCB.Core;

namespace HCB.Utilities
{
    [System.Serializable]
    public class PlayerData
    {

        public PlayerData()
        {
            CurrencyData = new Dictionary<ExchangeType, double>();
            CurrencyData[ExchangeType.Coin] = 0;            
        }

        private Dictionary<ExchangeType, double> currencyData = new Dictionary<ExchangeType, double>();
        [BoxGroup("Currency Data")]
        [ShowInInspector]
        [OnValueChanged("NotifyChange")]
        public Dictionary<ExchangeType, double> CurrencyData
        {
            get
            {
                return currencyData;
            }
            set
            {
                currencyData = value;
            }
        }        

        private void NotifyChange()
        {
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}