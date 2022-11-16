using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB;
using HCB.CollectableSystem;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;
using UnityEngine.Events;

public class Money : CollectableBase
{
    public bool IsCollected { get; protected set; }
    public bool IsJumped;
    public float MoneyValue { get; set; }
    public override bool CanCollect => !IsCollected;

    protected const float LOOP_OFFSET = -0.10f;
    protected const float LOOP_MOVE_DURATION = 0.5f;

    protected const float COLLECT_MOVEMENT_DURATION = 0.4f;

    protected string _moneyLoopTweenID;
    protected string _movementTweenID;

    private IncomeManager _incomeManager;

    public IncomeManager IncomeManager => _incomeManager == null
        ? _incomeManager = GetComponentInParent<IncomeManager>()
        : _incomeManager;

    [HideInInspector]
    public UnityEvent OnInitialized = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnCollected = new UnityEvent();
    

    protected virtual void Awake()
    {
        _movementTweenID = GetInstanceID() + "MovemeentTweenID";
        _moneyLoopTweenID = GetInstanceID() + "MoneyLoopTweenID";
       // MoneyValue = (int)IncomeUpgrade.Instance.IdleStat.CurrentValue;
       
    }
    
    public virtual void Initialize(int moneyValue) 
    {
        IsCollected = false;
       
        MoneyValue = moneyValue;
        //SetLoopAnimation();
        OnInitialized.Invoke();
    }
    
    public override void Collect(Collector collector)
    {
        if (IsCollected || !IsJumped)
            return;
        
        IsCollected = true;
        OnCollected.Invoke();
        //IsJumped = false;
        

        MovementTween(collector.transform, () => 
        {
            OnMoneyMovementCompleted(collector);
        });
    }

    protected void OnMoneyMovementCompleted(Collector collector)
    {
        GameManager.Instance.PlayerData.CurrencyData[HCB.ExchangeType.Coin] += IncomeManager.IdleStat.CurrentValue; 
        HCB.Core.EventManager.OnMoneyEarned.Invoke();
        HCB.Core.EventManager.OnPlayerDataChange.Invoke();
        base.Collect(collector);
        
        PoolingSystem.Instance.DestroyAPS(gameObject);
        
    }
    public void SetLoopAnimation()
    {
        DOTween.Kill(_moneyLoopTweenID);
        transform.DOMoveY(transform.position.y + LOOP_OFFSET, LOOP_MOVE_DURATION).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetId(_moneyLoopTweenID);
    }

    
    protected void MovementTween(Transform target, Action onComplete) 
    {
        DOTween.Kill(_moneyLoopTweenID);
        DOTween.Kill(_movementTweenID);

        transform.SetParent(target);
        transform.DOLocalMove(Vector3.zero, COLLECT_MOVEMENT_DURATION).SetEase(Ease.Linear).SetTarget(_movementTweenID).OnComplete(() => onComplete?.Invoke());
    }
    
}
