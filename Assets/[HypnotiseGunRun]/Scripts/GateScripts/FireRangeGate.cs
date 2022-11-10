using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class FireRangeGate : GateBase
{
  
    
    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponentInChildren<Interactor>();

        if (interactor != null)
        {
            HapticManager.Haptic(HapticTypes.RigidImpact);
            HCB.Core.EventManager.OnFireRangeGateInteracted.Invoke();
            //CreateParticle(interactor.transform);
            Debug.Log("FireRange"); 
            
            OnInteracted.Invoke();
        }
    }
    
    
}
