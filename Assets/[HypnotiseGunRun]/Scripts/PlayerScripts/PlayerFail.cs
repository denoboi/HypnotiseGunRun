using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using HCB.Core;
using UnityEngine;

public class PlayerFail : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Rigidbody Rigidbody => _rigidbody == null ? _rigidbody = Player.GetComponentInChildren<Rigidbody>() : _rigidbody;

    private SplineFollower _splineFollower;
    private SplineFollower SplineFollower => _splineFollower == null ? _splineFollower = Player.GetComponentInChildren<SplineFollower>() : _splineFollower;

    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;

   
    [SerializeField] private Collider _mainCollider;
   

    private const float PUSH_FORCE = 185f;
    private const float PUSH_DURATION = 0.75f;

    private Run _pushRun = null;

    private void OnEnable()
    {
        HCB.Core.EventManager.OnPlayerFailed.AddListener(StartPushBack);
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnPlayerFailed.RemoveListener(StartPushBack);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (Player.IsFailed)
            return;
      
        ObstacleDestruction breakable = other.GetComponent<ObstacleDestruction>();
        
        if (breakable != null)
        {
            HCB.Core.EventManager.OnPlayerFailed.Invoke();
            Player.IsFailed = true;
            StartPushBack();
         
           
         
            Run.After(1,()=>GameManager.Instance.CompeleteStage(false));
         
            HapticManager.Haptic(HapticTypes.Failure);
            Debug.Log("DIE MOTHERFUCKER DIE");
        }
    }
   
   
    private void StartPushBack() 
    {
        SplineFollower.motion.applyPosition = false;

        _mainCollider.isTrigger = false;
        Rigidbody.isKinematic = false;
       
        Rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        Rigidbody.AddForce(-transform.forward.normalized * PUSH_FORCE);

        if (_pushRun != null)
            _pushRun.Abort();

        _pushRun = Run.After(PUSH_DURATION, EndPushBack);
      
    }
   
    private void EndPushBack() 
    {
        
        Rigidbody.isKinematic = true;
        _mainCollider.isTrigger = true;        
    }
}
