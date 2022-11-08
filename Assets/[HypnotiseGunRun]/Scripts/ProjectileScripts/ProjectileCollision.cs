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

   
    // private void OnTriggerEnter(Collider other)
    // {
    //     ObstacleDestruction breakable = other.GetComponentInParent<ObstacleDestruction>();
    //
    //     if (breakable != null && !_isCollided)
    //     {
    //         _isCollided = true;
    //         HapticManager.Haptic(HapticTypes.SoftImpact);
    //
    //
    //         breakable.ObstacleLevel--;
    //         breakable.OnHit.Invoke();
    //
    //
    //         Debug.Log("Carptii");
    //
    //         if (breakable.ObstacleLevel <= 0)
    //         {
    //             breakable.ObstacleLevel = 0;
    //             breakable.DestructObstacle();
    //         }
    //     }
    // }

    private void OnCollisionEnter(Collision collision)
    {
        ObstacleDestruction breakable = collision.gameObject.GetComponentInParent<ObstacleDestruction>();

        if (breakable != null && !_isCollided && breakable.ObstacleLevel > 0)
        {
            _isCollided = true;
            HapticManager.Haptic(HapticTypes.SoftImpact);

            Debug.Log("Carptii");
            breakable.ObstacleLevel--;
            breakable.OnHit.Invoke();


            

            if (breakable.ObstacleLevel <= 0)
            {
                breakable.ObstacleLevel = 0;
                breakable.DestructObstacle();
            }
        }
    }

}