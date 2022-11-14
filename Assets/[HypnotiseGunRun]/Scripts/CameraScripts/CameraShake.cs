using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using HCB.Core;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin multiChannelPerlin;
    public CinemachineBasicMultiChannelPerlin MultiChannelPerlin { get { return multiChannelPerlin == null ? multiChannelPerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>() : multiChannelPerlin; } }

    private const float FAIL_INTENSITY = 5f;
    private const float FAIL_SHAKE_DURATION = 0.25f;

    private const float BASKET_INTENSITY = 1.5f;
    private const float BASKET_SHAKE_DURATION = 0.15f;

    private float _currentShakeTime;
    private float _startingInstensity;

    private void OnEnable()
    {
        HCB.Core.EventManager.OnPlayerFailed.AddListener(OnFailed);
       
    }

    private void OnDisable()
    {
       HCB.Core.EventManager.OnPlayerFailed.RemoveListener(OnFailed);
        
    }

    private void Update()
    {
        if (_currentShakeTime > 0)
        {
            _currentShakeTime -= Time.deltaTime;
            MultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_startingInstensity, 0f, (1 - (_currentShakeTime / FAIL_SHAKE_DURATION)));
        }
    }

    private void Shake(float intensity, float duration)
    {
        MultiChannelPerlin.m_AmplitudeGain = intensity;
        _startingInstensity = intensity;  
        _currentShakeTime = duration;
    }   
    
    private void OnFailed() 
    {
        Shake(FAIL_INTENSITY, FAIL_SHAKE_DURATION);
    }

    
}
