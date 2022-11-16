using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class PlayerFireRate : MonoBehaviour
{
    public static PlayerFireRate Instance;
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    
    [Range(0,100)]
    [SerializeField] private int _fireRate;
    public int FireRate
    {
        get => _fireRate;
        set => _fireRate = value;
    }
    public IdleStat FireRateIdleStat => UpgradeManager.Instance.FireRate;
    //private float InitialFireRate => (float)GameManager.Instance.UpgradeData.FireRateStat.CurrentValue;
    
    private const int MAX_FIRE_RATE = 100;
    private const int INCREASE_AMOUNT = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        HCB.Core.EventManager.OnFireRateGateInteracted.AddListener(IncreaseFireRate);
        LevelManager.Instance.OnLevelStart.AddListener(SetInitialFireRate);
        
    }
    
    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        HCB.Core.EventManager.OnFireRateGateInteracted.RemoveListener(IncreaseFireRate);
        LevelManager.Instance.OnLevelStart.RemoveListener(SetInitialFireRate);
    
    }
    
    
    
    private void IncreaseFireRate()
    {
        FireRate += INCREASE_AMOUNT;
        FireRate = Mathf.Min(FireRate, MAX_FIRE_RATE);
    }
    
    private void SetInitialFireRate()
    {
        FireRate = (int)FireRateIdleStat.CurrentValue;
    }
}
