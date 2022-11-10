using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [Range(1, 50)] [SerializeField] private int _health;

    public int Health
    {
        get => _health;
        set => _health = value;
    }

    private bool IsKilled { get; set; }

    [HideInInspector] public UnityEvent OnHit = new UnityEvent();
    [HideInInspector] public UnityEvent OnKilled = new UnityEvent();


    // private void OnCollisionEnter(Collision collision)
    // {
    //     Projectile _projectile = collision.gameObject.GetComponentInParent<Projectile>();
    //
    //     if (_projectile != null)
    //     {
    //        Hit();
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileCollision _projectile = other.GetComponentInParent<ProjectileCollision>();
       
        if (_projectile != null)
        {
            Hit();
            _projectile.Renderer.enabled = false;
        }
        
    }


    private void Hit()
    {
        OnHit.Invoke();
        Health--;
       
        Health = Mathf.Max(Health, 0);
        Debug.Log("ENEMYBAM");
        CheckKill();
    }

    private void CheckKill()
    {
        if (Health > 0) return;
        Kill();
    }

    private void Kill()
    {
        if (IsKilled) return;
        
        IsKilled = true;
        OnKilled.Invoke();
    }
}


