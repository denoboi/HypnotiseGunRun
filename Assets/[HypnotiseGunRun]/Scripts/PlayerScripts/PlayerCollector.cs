using System.Collections;
using System.Collections.Generic;
using HCB.CollectableSystem;
using UnityEngine;

public class PlayerCollector : Collector
{
    private void OnEnable()
    {
      
        //HCB.Core.EventManager.OnPlayerFailed.AddListener(() => CanCollect = false); 
    }

    private void OnDisable()
    {
        //HCB.Core.EventManager.OnPlayerFailed.RemoveListener(() => CanCollect = false);
    }
}
