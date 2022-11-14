using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using HCB.Core;
public class CameraBrain : MonoBehaviour
{
    private CinemachineBrain brain;
    public CinemachineBrain CinemachineBrain { get { return brain == null ? brain = GetComponent<CinemachineBrain>() : brain; } }

    private void OnEnable()
    {
        if (Managers.Instance == null) return;
        CameraManager.Instance.CameraBrain = this;
    }
}