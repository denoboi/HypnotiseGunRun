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

        public GameObject CollectEffectPrefab;

        public virtual void Collect(Collector collector)
        {
            if (IsCollected)
                return;

            IsCollected = true;

            if (CollectEffectPrefab != null)
            {
                ParticleSystem collectEffect = Instantiate(CollectEffectPrefab, transform.position, transform.rotation).GetComponent<ParticleSystem>();
                var main = collectEffect.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }

            EventManager.OnFloatingCollectableCollected.Invoke(transform.position, OnFloatingMovementCompleted);
            HapticManager.Haptic(HapticTypes.RigidImpact);
            Destroy(gameObject);
        }

        public virtual void OnFloatingMovementCompleted() 
        {
            PlayerData playerData = GameManager.Instance.PlayerData;
            playerData.CurrencyData[ExchangeType.Coin]++;
            EventManager.OnPlayerDataChange.Invoke();
        }
    }
}
