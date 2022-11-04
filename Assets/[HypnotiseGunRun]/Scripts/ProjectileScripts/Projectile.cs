using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = GetComponentInChildren<Rigidbody>() : _rigidbody;

    
    
    [HideInInspector]
    public UnityEvent OnInitialized = new UnityEvent();
    
    [HideInInspector]
    public UnityEvent OnKilled = new UnityEvent();


  
    public Vector3 Direction { get; private set; }

    public float ForceAmount { get; set; }
    
    public void Initialize(Vector3 direction)
    {
        Direction = direction;

        OnInitialized.Invoke();
        
    }

   
}
