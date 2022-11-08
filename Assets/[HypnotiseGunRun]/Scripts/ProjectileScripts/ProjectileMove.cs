using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using HCB.Core;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectileMove : MonoBehaviour
{
    private Projectile _projectile;
    public Projectile Projectile
    {
        get { return _projectile == null ? _projectile = GetComponentInChildren<Projectile>() : _projectile; }
    }

    

    private void OnEnable()
    {
        if (Projectile == null)
            return;
        Projectile.OnInitialized.AddListener(MoveProjectile);
    }

    private void OnDisable()
    {
        if (Projectile == null)
            return;
        Projectile.OnInitialized.RemoveListener(MoveProjectile);
    }

    private Rigidbody _rb;

    public Rigidbody Rigidbody => _rb == null ? _rb = GetComponentInChildren<Rigidbody>() : _rb;

    private bool _canShoot = true;
    [SerializeField] private float _speed = 20f;

    private void Update()
    {
       // MoveProjectile();
    }
    
    [Button]
    private void MoveProjectile()
    {
        //if (!_canShoot) return;
        Rigidbody.AddForce((Vector3.up / 2f + Vector3.forward) * _speed, ForceMode.Impulse);
        
        //transform.Translate(Projectile.Direction * _speed * Time.deltaTime);
    }
    
    
    
}
