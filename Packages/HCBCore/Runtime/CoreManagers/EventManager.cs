using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using HCB.Utilities;
using System;

namespace HCB.Core
{
    public static class EventManager
    {
        public static UnityEvent OnPlayerDataChange = new UnityEvent();
        public static CurrencyEvent OnCurrencyInteracted = new CurrencyEvent();
        public static FloatingCollectableEvent OnFloatingCollectableCollected = new FloatingCollectableEvent();

        #region Editor
        public static UnityEvent OnLevelDataChange = new UnityEvent();
        #endregion
    }

    public class PlayerDataEvent : UnityEvent<PlayerData> { }
    public class CurrencyEvent : UnityEvent<ExchangeType, int> { }
    public class FloatingCollectableEvent : UnityEvent<Vector3, Action> { }
}