using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;
using UnityEngine.Events;

public abstract class GateBase : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; }

    [HideInInspector]
    public UnityEvent OnInteracted = new UnityEvent();
    
    private string _currentParticleID = DEFAULT_GATE_POOL_PARTICLE;
    public string CurrentParticleID { get => _currentParticleID; protected set => _currentParticleID = value; }

    protected const string DEFAULT_GATE_POOL_PARTICLE = "GateParticle"; 
    

   
    protected void CreateParticle(Transform parent) 
    {
        GameObject particleObject = PoolingSystem.Instance.InstantiateAPS(CurrentParticleID, parent.position);
       particleObject.transform.SetParent(parent);
        particleObject.GetComponentInChildren<ParticleSystem>().Play();
        
        
    }   

    protected virtual void GateInteract(Transform parent)
    {
        Debug.Log("InteractedGate");
        HapticManager.Haptic(HapticTypes.Selection);
        //CreateParticle(parent);
        
    }
    

   
}

