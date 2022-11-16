using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class SpreadShotGate : GateBase
{
    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponentInChildren<Interactor>();

        if (interactor != null)
        {
           CreateParticle(interactor.transform);
           
            HapticManager.Haptic(HapticTypes.RigidImpact);
            
            HCB.Core.EventManager.OnSpreadShotGateInteracted.Invoke();
            OnInteracted.Invoke();
            Debug.Log("SpreadShot");
        }
    }
}
