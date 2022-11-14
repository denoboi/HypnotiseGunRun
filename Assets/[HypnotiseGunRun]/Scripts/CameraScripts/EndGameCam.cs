using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class EndGameCam : VirtualCameraBase
{
    private const float BLEND_DURATION = 0.5f;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (Managers.Instance == null)
            return;

        HCB.Core.EventManager.OnEnteredEndGame.AddListener(ActivateCamera);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (Managers.Instance == null)
            return;

        HCB.Core.EventManager.OnEnteredEndGame.RemoveListener(ActivateCamera);
    }

    private void ActivateCamera()
    {
        CameraManager.Instance.ActivateCamera(this, BLEND_DURATION);
    }
}
