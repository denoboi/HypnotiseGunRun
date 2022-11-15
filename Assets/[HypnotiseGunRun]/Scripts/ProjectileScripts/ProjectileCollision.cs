using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using HCB.PoolingSystem;
using TMPro;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private bool _isCollided;
    private bool _isCollidedWithMirror;
    private MeshRenderer _renderer;
    public MeshRenderer Renderer => _renderer == null ? _renderer = GetComponent<MeshRenderer>() : _renderer;
    

    private void OnTriggerEnter(Collider other)
    {
        ObstacleDestruction breakable = other.GetComponentInParent<ObstacleDestruction>();
        BreakableMirror mirror = other.GetComponentInParent<BreakableMirror>();
    
        if (breakable != null && !_isCollided)
        {
            DestructObstacle();
    
            breakable.ObstacleLevel--;
            breakable.OnHit.Invoke();
         
            if (breakable.ObstacleLevel <= 0)
            {
                breakable.ObstacleLevel = 0;
                breakable.DestructObstacle();
            }
        }

        if (mirror != null && !_isCollidedWithMirror)
        {
            MirrorText mirrorText = mirror.GetComponentInChildren<MirrorText>();
            
            MirrorHitCheck();

            mirror.MirrorLevel--;
            mirrorText.SetDurability(mirror.MirrorLevel);
            mirror.OnHit.Invoke();

            if (mirror.MirrorLevel <= 0)
            {
                mirror.MirrorLevel = 0;
                mirror.DestructMirror();
            }
            
        }
    }

    void DestructObstacle()
    {
        _isCollided = true;
        HapticManager.Haptic(HapticTypes.SoftImpact);
        Renderer.enabled = false;

    }

    void MirrorHitCheck()
    {
        _isCollidedWithMirror = true;
        HapticManager.Haptic(HapticTypes.HeavyImpact);
        Renderer.enabled = false;
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     ObstacleDestruction breakable = collision.gameObject.GetComponentInParent<ObstacleDestruction>();
    //
    //     if (breakable != null && !_isCollided && breakable.ObstacleLevel > 0)
    //     {
    //         _isCollided = true;
    //         HapticManager.Haptic(HapticTypes.SoftImpact);
    //         
    //         Debug.Log("Carptii");
    //         breakable.ObstacleLevel--;
    //         breakable.OnHit.Invoke();
    //         
    //        
    //
    //
    //         
    //
    //         if (breakable.ObstacleLevel <= 0)
    //         {
    //             breakable.ObstacleLevel = 0;
    //             breakable.DestructObstacle();
    //         }
    //     }
    // }

}