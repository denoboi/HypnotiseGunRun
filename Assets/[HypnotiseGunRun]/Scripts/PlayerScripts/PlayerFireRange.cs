using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class PlayerFireRange : IdleStatObjectBase
{
    public static PlayerFireRange Instance;
    public float DestroyTime { get; set; }
    private const float INCREASE_AMOUNT = .3f;

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
       
        HCB.Core.EventManager.OnFireRangeGateInteracted.AddListener(IncreaseProjectileRange);
        LevelManager.Instance.OnLevelStart.AddListener(SetInitialProjectileRange);
        
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
       
        HCB.Core.EventManager.OnFireRangeGateInteracted.RemoveListener(IncreaseProjectileRange);
        LevelManager.Instance.OnLevelStart.AddListener(SetInitialProjectileRange);

    }

    public override void UpdateStat(string id)
    {
        throw new NotImplementedException();
    }

    private void IncreaseProjectileRange()
    {
        DestroyTime += INCREASE_AMOUNT;
    }

    private void SetInitialProjectileRange()
    {
        DestroyTime = (float)IdleStat.CurrentValue;
    }
}
