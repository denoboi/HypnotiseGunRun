using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class ProjectileCreator : MonoBehaviour
{
    private Player _player;

    public Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    public const string PROJECTILE_POOL_ID = "ProjectileBalls";
    
   

    [SerializeField] private Transform _projectileSpawnPoint;
    

    public Projectile CreateProjectile()
    {
        if (Player.IsFailed || Player.IsWin)
            return null;

        Projectile projectile = PoolingSystem.Instance.InstantiateAPS(PROJECTILE_POOL_ID, _projectileSpawnPoint.position)
            .GetComponentInChildren<Projectile>();
        
        //projectile.transform.SetParent(transform);
        
        projectile.Initialize(Vector3.forward);
        
        return projectile;
    }
}