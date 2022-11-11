using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    private PlayerProjectileCreator _playerProjectileCreator;

    public PlayerProjectileCreator PlayerProjectileCreator
    {
        get
        {
            return _playerProjectileCreator == null
                ? _playerProjectileCreator = GetComponentInChildren<PlayerProjectileCreator>()
                : _playerProjectileCreator;
        }
    }

    private PlayerFireRate _playerFireRate;

    public PlayerFireRate PlayerFireRate => _playerFireRate == null
        ? _playerFireRate = GetComponentInParent<PlayerFireRate>()
        : _playerFireRate;
    
    private PlayerSpreadShot _playerSpreadShot;

    public PlayerSpreadShot PlayerSpreadShot
    {
        get
        {
            return _playerSpreadShot == null
                ? _playerSpreadShot = GetComponentInChildren<PlayerSpreadShot>()
                : _playerSpreadShot;
        }
    }

    
    private bool _isGameStarted;
    private bool _isGameEnd;
    [Range(0.1f, 10f)]
    public float SpawnRate;
    private float _timer = Mathf.Infinity;

    private void OnEnable()
    {
        if (Managers.Instance == null) return;

        LevelManager.Instance.OnLevelStart.AddListener(() => Run.After(1,()=>_isGameStarted = true));
        GameManager.Instance.OnGameEnd.AddListener(() => _isGameStarted = false);
        
        //HCB.Core.EventManager.OnPlayerSuccess.AddListener(()=> _isGameEnd = true);
        
    }

    private void OnDisable()
    {
        if (Managers.Instance == null) return;

        LevelManager.Instance.OnLevelStart.RemoveListener(() => Run.After(1,()=>_isGameStarted = true));
        GameManager.Instance.OnGameEnd.RemoveListener(() => _isGameStarted = false);
       // HCB.Core.EventManager.OnPlayerSuccess.RemoveListener(()=> _isGameEnd = true);
    }

    private void Start()
    {
        //SpawnProjectile();
    }


    private void Update()
    {
        SpawnProjectile();
        ProjectileSpawnRate();
    }

    private void ProjectileSpawnRate()
    {
       // SpawnRate = 1 / PlayerFireRate.FireRate;
    }

    private void SpawnProjectile()
    {
        if (!_isGameStarted)
            return;
        
        if (_isGameEnd)
            return;
        
        if (Player.Instance.IsWin)
            return;
        
        _timer += Time.deltaTime;

        if (_timer >= SpawnRate)
        {
             PlayerProjectileCreator.CreateProjectile();
            //
            // if (PlayerSpreadShot.IsSpreadShotEnabled)
            // {
            //     PlayerSpreadShot.SpreadShotSpawn();
            // }

            HCB.Core.EventManager.OnShoot.Invoke();
            _timer = 0;
        }
    }
}