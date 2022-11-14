using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using Sirenix.OdinInspector;
using HCB.Core;
using HCB.Utilities;
public class CameraManager : Singleton<CameraManager>
{
    [ReadOnly]
    public List<VirtualCameraBase> VirtualCameras = new List<VirtualCameraBase>();

    [ReadOnly]
    public CameraBrain CameraBrain;

    private List<string> _tpsCameraBlockers = new List<string>();
    public List<string> TpsCameraBlockers {  get { return _tpsCameraBlockers; }  private set { _tpsCameraBlockers = value; } }
    
    public VirtualCameraBase CurrentActiveCamera { get; private set; }
    private void OnEnable()
    {
        SceneController.Instance.OnSceneStartedLoading.AddListener(ResetList);
    }

    private void OnDisable()
    {
        SceneController.Instance.OnSceneStartedLoading.RemoveListener(ResetList);
    }

    public void AddCamera(VirtualCameraBase virtualCamera) 
    {
        if (!VirtualCameras.Contains(virtualCamera))
        {
            VirtualCameras.Add(virtualCamera);
        }
    }

    public void RemoveCamera(VirtualCameraBase virtualCamera) 
    {
        if (VirtualCameras.Contains(virtualCamera))
        {
            VirtualCameras.Remove(virtualCamera);
        }
    }
    public void ActivateCamera(VirtualCameraBase virtualCamera, float blendTime)
    {
        if (CurrentActiveCamera == virtualCamera)
            return;

        CameraBrain.CinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, blendTime);
        virtualCamera.Camera.Priority = 20;
        for (int i = 0; i < VirtualCameras.Count; i++)
        {
            if (VirtualCameras[i] == virtualCamera) continue;
            VirtualCameras[i].Camera.Priority = 10;
        }

        CurrentActiveCamera = virtualCamera;
    }
    public void AddTpsCameraBlocker(string blockerId) 
    {
        if (!TpsCameraBlockers.Contains(blockerId))
        {
            TpsCameraBlockers.Add(blockerId);
        }
    }

    public void RemoveTpsCameraBlocker(string blockerId) 
    {
        if (TpsCameraBlockers.Contains(blockerId))
        {
            TpsCameraBlockers.Remove(blockerId);
        }
    }
    private void ResetList()
    {
        VirtualCameras.Clear();
    }
}
