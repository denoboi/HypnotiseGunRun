using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElephantSDK;
using HCB.Utilities;
using HCB.Core;
using HCB.IncrimantalIdleSystem;
using Newtonsoft.Json;

public class RemoteConfigManager : Singleton<RemoteConfigManager>
{
   
    private const float DEFAULT_IDLE_INTERSTITIAL_DELAY = 40;

    private void OnEnable()
    {
        ElephantCore.onRemoteConfigLoaded += ReciveRemoteConfigData;
        Debug.Log("Sub to Remote config");
    }

    private void OnDisable()
    {
        ElephantCore.onRemoteConfigLoaded -= ReciveRemoteConfigData;
    }

    private void ReciveRemoteConfigData()
    {
        Debug.Log("remote config load");


        HCB.Core.EventManager.OnRemoteUpdated.Invoke();
    }
}
