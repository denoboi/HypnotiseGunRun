using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;

public class ProjectileDestroyer : MonoBehaviour
{
    private float _timer = 0;

    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInParent<Rigidbody>() : _rigidbody;

    private List<Rigidbody> _rigidbodies;
    
    private Projectile _projectile;



    private float DestroyTime { get; set; } = 3;

    public Projectile Projectile
    {
        get { return _projectile == null ? _projectile = GetComponentInChildren<Projectile>() : _projectile; }
    }


    public List<Rigidbody> Rigidbodies =>
        _rigidbodies ??= GetComponentsInChildren<Rigidbody>().ToList();

   
    private void OnEnable()
    {
        Projectile.OnInitialized.AddListener(StartDestroy);
    }
    
    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        Projectile.OnInitialized.RemoveListener(StartDestroy);
    }

    private void StartDestroy()
    {
       // Run.After(PlayerFireRange.Instance.DestroyTime, DestroyProjectile); //update yerine boyle yap
       Run.After(DestroyTime, ()=> PoolingSystem.Instance.DestroyAPS(gameObject));
    }

   


    private void DestroyProjectile()
    {
        // foreach (var rb in Rigidbodies)
        // {
        //     rb.constraints = RigidbodyConstraints.None;
        // }
        Projectile.OnKilled.Invoke(); //yoksa bug oldu.
        
    }
}