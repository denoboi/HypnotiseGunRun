using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ObstacleIncome : MonoBehaviour
{
    private ObstacleDestruction _obstacleDestruction;

    public ObstacleDestruction ObstacleDestruction => _obstacleDestruction == null
        ? _obstacleDestruction = GetComponent<ObstacleDestruction>()
        : _obstacleDestruction;


    private const string BIG_MONEY_POOL_ID = "BigMoney";
    private const string MONEY_POOL_ID = "Money";
    private const float SPAWN_OFFSET = 0.5f;
    private const int MONEY_VALUE = 1;

    private void OnEnable()
    {
        ObstacleDestruction.OnObstacleDestroyed.AddListener(SpawnMoney);
        ObstacleDestruction.OnBigObstacleDestroyed.AddListener(SpawnBigMoney);
    }

    private void OnDisable()
    {
        ObstacleDestruction.OnObstacleDestroyed.RemoveListener(SpawnMoney);
        ObstacleDestruction.OnBigObstacleDestroyed.RemoveListener(SpawnBigMoney);
    }

    private void SpawnMoney() 
    {
        // Vector3 spawnPoint = transform.position + Vector3.up * SPAWN_OFFSET;
        // Money money = PoolingSystem.Instance.InstantiateAPS(MONEY_POOL_ID, spawnPoint).GetComponentInChildren<Money>();
        // money.Initialize(MONEY_VALUE);
    }

    private void SpawnBigMoney()
    {
        // Vector3 spawnPoint = transform.position + Vector3.up * SPAWN_OFFSET;
        // Money money2 = PoolingSystem.Instance.InstantiateAPS(BIG_MONEY_POOL_ID, spawnPoint).GetComponentInChildren<Money>();
        // money2.Initialize(MONEY_VALUE);
    }
}
