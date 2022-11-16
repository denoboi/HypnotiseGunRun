using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class FireRateGate : GateBase
{
  
   private void OnTriggerEnter(Collider other)
   {
      Interactor interactor = other.GetComponentInChildren<Interactor>();
   
      if (interactor != null)
      {
         
         CreateParticle(interactor.transform);
         HCB.Core.EventManager.OnFireRateGateInteracted.Invoke();
         Debug.Log("FireRate");
       
         OnInteracted.Invoke();
         GateInteract(interactor.transform);
      }
   }

   
}
