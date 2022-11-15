using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class ObstacleIncome : MonoBehaviour
{
    private ObstacleDestruction _obstacleDestruction;
    [SerializeField] private List<Money> _moneys = new List<Money>();

    public ObstacleDestruction ObstacleDestruction => _obstacleDestruction == null
        ? _obstacleDestruction = GetComponent<ObstacleDestruction>()
        : _obstacleDestruction;


    private const string BIG_MONEY_POOL_ID = "BigMoney";
    private const string MONEY_POOL_ID = "Money";
    private const float SPAWN_OFFSET = 0.5f;
    private const int MONEY_VALUE = 1;
    private string _movementTweenID;

    private void Awake()
    {
        _movementTweenID = GetInstanceID() + "MovementTweenID";
    }

    private void OnEnable()                                                 
    {
        ObstacleDestruction.OnObstacleDestroyed.AddListener(MoveMoney);
       // ObstacleDestruction.OnBigObstacleDestroyed.AddListener(SpawnBigMoney);
    }

    private void OnDisable()
    {
        ObstacleDestruction.OnObstacleDestroyed.RemoveListener(MoveMoney);
        //ObstacleDestruction.OnBigObstacleDestroyed.RemoveListener(SpawnBigMoney);
    }


    private void MoveMoney()
    {
        DOTween.Kill(_movementTweenID);
       
       

        foreach (var money in _moneys)
        {
            if (_moneys == null)
                return;
            
            Vector3 movingMoneyPos = new Vector3(Random.Range(-1f, 1f), .65f, Random.Range(2f,3f));
            
            Money jumpingMoney = money.GetComponent<Money>();
            
            jumpingMoney.transform.DOJump(transform.position + movingMoneyPos, 1.3f, 1, 1).OnComplete(() => EndOfJump(jumpingMoney));
            jumpingMoney.Initialize(MONEY_VALUE);
        }
       
    }

    private void EndOfJump(Money money)
    {
        
        money.IsJumped = true;
        money.SetLoopAnimation();
    }
    
    // private void SpawnMoney() 
    // {
    //     Vector3 spawnPoint = transform.position + Vector3.up * SPAWN_OFFSET;
    //     Money money = PoolingSystem.Instance.InstantiateAPS(MONEY_POOL_ID, spawnPoint).GetComponentInChildren<Money>();
    //     money.Initialize(MONEY_VALUE);
    // }

    private void SpawnBigMoney()
    {
        // Vector3 spawnPoint = transform.position + Vector3.up * SPAWN_OFFSET;
        // Money money2 = PoolingSystem.Instance.InstantiateAPS(BIG_MONEY_POOL_ID, spawnPoint).GetComponentInChildren<Money>();
        // money2.Initialize(MONEY_VALUE);
    }
}
