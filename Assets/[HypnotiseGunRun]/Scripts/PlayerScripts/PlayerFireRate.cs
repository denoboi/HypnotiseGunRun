using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class PlayerFireRate : MonoBehaviour
{
    // private Player _player;
    // private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;   
    // public float FireRate { get; set; }
    // public IdleStat FireRateIdleStat => UpgradeManager.Instance.FireRate;
    //
    // //private float InitialFireRate => (float)GameManager.Instance.UpgradeData.FireRateStat.CurrentValue;
    //
    // private const float MAX_FIRE_RATE = 100f;
    // private const float INCREASE_AMOUNT = 1f;
    //
    //
    // private void OnEnable()
    // {
    //     HCB.Core.EventManager.OnFireRateGateInteracted.AddListener(IncreaseFireRate);
    //     LevelManager.Instance.OnLevelStart.AddListener(SetInitialFireRate);
    //     
    // }
    //
    // private void OnDisable()
    // {
    //     if (Managers.Instance == null)
    //         return;
    //     HCB.Core.EventManager.OnFireRateGateInteracted.RemoveListener(IncreaseFireRate);
    //     LevelManager.Instance.OnLevelStart.RemoveListener(SetInitialFireRate);
    //
    // }
    //
    //
    //
    // private void IncreaseFireRate()
    // {
    //     FireRate += INCREASE_AMOUNT;
    //     FireRate = Mathf.Min(FireRate, MAX_FIRE_RATE);
    // }
    //
    // private void SetInitialFireRate()
    // {
    //     FireRate = (float)FireRateIdleStat.CurrentValue;
    // }
}
