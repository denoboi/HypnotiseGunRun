using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using UnityEngine;
using UnityEngine.Events;

public class GateBase : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; }

    [HideInInspector]
    public UnityEvent OnInteracted = new UnityEvent();
    
    private string _currentParticleID = DEFAULT_GATE_POOL_PARTICLE;
    public string CurrentParticleID { get => _currentParticleID; protected set => _currentParticleID = value; }

    protected const string DEFAULT_GATE_POOL_PARTICLE = "PositiveGateParticle"; 
    

    protected virtual void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponentInChildren<Interactor>();

        if (interactor != null)
        {
            CreateParticle(interactor.transform);
            Debug.LogError("Interacted Gate");
        }
    }

    protected virtual void CreateParticle(Transform parent) 
    {
        GameObject particleObject = PoolingSystem.Instance.InstantiateAPS(CurrentParticleID, parent.position);
        particleObject.transform.SetParent(parent);
        particleObject.GetComponentInChildren<ParticleSystem>().Play();
    }   

   
}

