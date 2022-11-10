using HCB.Core;
using HCB;
using HCB.FloatingCollectableSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIncome : MonoBehaviour
{
    // private Enemy _enemy;
    // private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();
    //
    // private const float UP_OFFSET = 2f;    
    // private const float MONEY_COUNT = 3f;
    // private const float RANDOM_UNIT_CIRCLE_OFFSET = 1f;
    //
    // private const float MOVEMENT_DURATION = 1f;
    // private const float MOVEMENT_MIN_DELAY = 0.05f;
    // private const float MOVEMENT_MAX_DELAY = 0.15f;
    //
    // [SerializeField] private int _income;
    //
    // private void OnEnable()
    // {
    //     Enemy.OnKilled.AddListener(CheckIncome);
    // }
    //
    // private void OnDisable()
    // {
    //     Enemy.OnKilled.RemoveListener(CheckIncome);
    // }
    //
    // private void CheckIncome() 
    // {
    //     float moneyAmount = _income / MONEY_COUNT;
    //     for (int i = 0; i < MONEY_COUNT; i++)
    //     {
    //         float delay = Random.Range(MOVEMENT_MIN_DELAY, MOVEMENT_MAX_DELAY);
    //         Vector3 position = transform.position + Vector3.up * UP_OFFSET + (Vector3)Random.insideUnitCircle * RANDOM_UNIT_CIRCLE_OFFSET;
    //         FloatingCollectablePanel.Instance.CreateFloatingMoney(position, MOVEMENT_DURATION, delay, () => OnFloatingMoneyMovementCompleted(moneyAmount));
    //     }
    // }
    //
    // private void OnFloatingMoneyMovementCompleted(float moneyAmount) 
    // {        
    //     GameManager.Instance.PlayerData.CurrencyData[ExchangeType.Coin] += moneyAmount;        
    //     HCB.Core.EventManager.OnPlayerDataChange.Invoke();
    //     HapticManager.Haptic(HapticTypes.RigidImpact);
    // }
}
