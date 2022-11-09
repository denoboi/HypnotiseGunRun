using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HCB.CollectableSystem;
using HCB.Core;
using HCB.Utilities;


namespace HCB.FloatingCollectableSystem
{
    public class FloatingCollectableBase : MonoBehaviour, ICollectable
    {
        private bool _isCollected;
        public bool IsCollected { get { return _isCollected; } set { _isCollected = value; } }

        public int CurrencyAmount = 1;
        public GameObject CollectEffectPrefab;

        public virtual void Collect(Collector collector)
        {
            if (IsCollected)
                return;

            IsCollected = true;

            if (CollectEffectPrefab != null)
            {
                ParticleSystem collectEffect = Instantiate(CollectEffectPrefab, transform.position, transform.rotation).GetComponentInChildren<ParticleSystem>();
                var main = collectEffect.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }

            FloatingCollectableEventManager.OnFloatingCollectableCollected.Invoke(transform.position, OnFloatingMovementCompleted);
            HapticManager.Haptic(HapticTypes.RigidImpact);
            Destroy(gameObject);
        }

        public virtual void OnFloatingMovementCompleted() 
        {
            PlayerData playerData = GameManager.Instance.PlayerData;
            playerData.CurrencyData[ExchangeType.Coin] += CurrencyAmount;
            HapticManager.Haptic(HapticTypes.RigidImpact);
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}
