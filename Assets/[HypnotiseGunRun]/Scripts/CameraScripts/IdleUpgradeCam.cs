using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class IdleUpgradeCam : VirtualCameraBase
{
    private const float BLEND_DURATION = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Managers.Instance == null)
            return;

        SceneController.Instance.OnSceneLoaded.AddListener(ActivateCamera);
        HCB.Core.EventManager.OnReachedChest.AddListener(ActivateCamera);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (Managers.Instance == null)
            return;

        SceneController.Instance.OnSceneLoaded.RemoveListener(ActivateCamera);
        HCB.Core.EventManager.OnReachedChest.RemoveListener(ActivateCamera);

    }

    private void ActivateCamera() 
    {
        CameraManager.Instance.ActivateCamera(this, BLEND_DURATION);
    }
}
