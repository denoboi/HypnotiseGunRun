using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class PlayerProjectileCreator : MonoBehaviour
{
    private Player _player;

    public GameObject ProjectilePrefab;

    public Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;
    public const string PROJECTILE_POOL_ID = "ProjectileBalls";


    [SerializeField] private Transform _projectileSpawnPoint;
    public Vector3 ShootDirection = new Vector3(0, .5f, 1f);

    public Projectile CreateProjectile()
    {
        if (Player.IsFailed || Player.IsWin)
            return null;


        Projectile projectile =
            Instantiate(ProjectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.localRotation)
                .GetComponent<Projectile>();


        projectile.GetComponentInChildren<MeshRenderer>().enabled = true;


        projectile.Initialize(ShootDirection); 

        return projectile;
        //projectile.transform.SetParent(transform);

    }
}