using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using HCB.Utilities;

namespace HCB.Core
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDataChange = new UnityEvent();
        public static CurrencyEvent OnCurrencyInteracted = new CurrencyEvent();

        public static StringEvet OnStatUpdated = new StringEvet();
        public static UnityEvent OnRemoteUpdated = new UnityEvent();
        public static AnalyticEvent OnLogEvent = new AnalyticEvent();
        
        public static UnityEvent OnPlayerFailed = new UnityEvent();

        public static UnityEvent OnFireRateGateInteracted = new UnityEvent();
        public static UnityEvent OnSpreadShotGateInteracted = new UnityEvent();
        public static UnityEvent OnFireRangeGateInteracted = new UnityEvent();
        
        public static UnityEvent OnMoneyEarned = new UnityEvent();
        public static UnityEvent OnEnteredEndGame = new UnityEvent();
        public static UnityEvent OnShoot = new UnityEvent();
        public static UnityEvent OnPlayerUpgraded = new UnityEvent();
        
        public static UnityEvent OnReachedChest = new UnityEvent();


        #region Editor
        public static UnityEvent OnLevelDataChange = new UnityEvent();
        #endregion
    }

    public class PlayerDataEvent : UnityEvent<PlayerData> { }
    public class CurrencyEvent : UnityEvent<ExchangeType, double> { }
    public class StringEvet : UnityEvent<string> { }
    public class AnalyticEvent : UnityEvent<string, string, string> { }

}