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
    

    public void CreateProjectile(bool isMultiple, int bulletCount)
    {
        
        if (Player.IsFailed || Player.IsWin)
            return;

        if (!isMultiple)
        {
            Projectile projectile = PoolingSystem.Instance.InstantiateAPS(PROJECTILE_POOL_ID, _projectileSpawnPoint.position)
                .GetComponentInChildren<Projectile>();

            projectile.GetComponentInChildren<MeshRenderer>().enabled = true;
            
            
            projectile.Initialize(Vector3.forward);
        
            
        }

        else
        {
            for (int i = 0; i < bulletCount; i++)
            {
                Projectile projectile = PoolingSystem.Instance.InstantiateAPS(PROJECTILE_POOL_ID, _projectileSpawnPoint.position)
                    .GetComponentInChildren<Projectile>();
                
                projectile.Initialize(Vector3.forward);
                projectile.GetComponentInChildren<MeshRenderer>().enabled = true;

                
            }

            
        }
        
       
        
        
        
        //projectile.transform.SetParent(transform);
        
       
    }
}