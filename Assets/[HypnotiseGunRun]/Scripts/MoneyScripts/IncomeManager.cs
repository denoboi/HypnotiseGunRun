using System;
using System.Collections;
using System.Collections.Generic;
using HCB.IncrimantalIdleSystem;
using UnityEngine;

public class IncomeManager : IdleStatObjectBase
{
    public static IncomeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private float IncomeRate;
    public override void UpdateStat(string id)
    {
        IncomeRate = (float)IdleStat.CurrentValue;
    }
}
