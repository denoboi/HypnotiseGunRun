using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using HCB.SplineMovementSystem;
using UnityEngine;

public class Player : SplineCharacter
{
    
    public bool IsFailed { get; set; }
    public bool IsWin { get; set; }
    
    public static Player Instance;
    
    private SplineCharacterMovementController _movementController;

    public SplineCharacterMovementController MovementController => _movementController == null
        ? _movementController = GetComponent<SplineCharacterMovementController>()
        : _movementController;

    //private const string UPGRADE_PARTICLE_ID = "UpgradeParticle";
    
    protected override void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        base.OnEnable();
        
       HCB.Core.EventManager.OnPlayerFailed.AddListener(OnLevelEnd);
        //HCB.Core.EventManager.OnPlayerUpgraded.AddListener((() => CreateParticle(UPGRADE_PARTICLE_ID)));
       HCB.Core.EventManager.OnEnteredEndGame.AddListener(SetSpeed);

        
    }

    protected override void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        base.OnDisable();
        HCB.Core.EventManager.OnPlayerFailed.RemoveListener(OnLevelEnd);

        HCB.Core.EventManager.OnEnteredEndGame.RemoveListener(SetSpeed);

        
        
    }
    
    private void CreateParticle(string poolId)
    {
        PoolingSystem.Instance.InstantiateAPS(poolId, transform.position).GetComponentInChildren<ParticleSystem>().Play();
    }

    private void SetSpeed()
    {
        MovementController.CurrentSpeed = 6f;
    }

    private void Awake()
    {
        Instance = this;
    }
}
