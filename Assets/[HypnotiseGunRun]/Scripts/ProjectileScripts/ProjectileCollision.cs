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


    private void OnTriggerEnter(Collider other)
    {
        ObstacleDestruction breakable = other.GetComponentInParent<ObstacleDestruction>();

        if (breakable != null && !_isCollided)
        {
            _isCollided = true;
            HapticManager.Haptic(HapticTypes.SoftImpact);


            breakable.ObstacleLevel--;
            breakable.OnHit.Invoke();


            Debug.Log("Carptii");

            if (breakable.ObstacleLevel <= 0)
            {
                breakable.ObstacleLevel = 0;
                breakable.DestructObstacle();
            }
        }
    }

//     private void OnCollisionEnter(Collision collision)
//     {
//         Ground ground = collision.collider.GetComponentInParent<Ground>();
//
//         if (ground != null)
//         {
//             Debug.Log("becerdii");
// //            RagdollController.EnableRagdollWithForce(Vector3.forward, 55);
//         }
}