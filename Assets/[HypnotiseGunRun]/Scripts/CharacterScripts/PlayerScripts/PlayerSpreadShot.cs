using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadShot : MonoBehaviour
{
    // private ProjectileCreator _projectileCreator;
    //
    //
    // public ProjectileCreator ProjectileCreator
    // {
    //     get
    //     {
    //         return _projectileCreator == null
    //             ? _projectileCreator = GetComponentInChildren<ProjectileCreator>()
    //             : _projectileCreator;
    //     }
    // }
    //
    // private const float SPREAD_SHOT_OFFSET_X = .1f;
    //
    //
    //
    // public bool IsSpreadShotEnabled { get; private set; }
    //
    // private void OnEnable()
    // {
    //     HCB.Core.EventManager.OnSpreadShotGateInteracted.AddListener(EnableSpreadShot);
    // }
    //
    // private void OnDisable()
    // {
    //     HCB.Core.EventManager.OnSpreadShotGateInteracted.RemoveListener(EnableSpreadShot);
    // }
    //
    // public void SpreadShotSpawn()
    // {
    //     if (Player.Instance.IsFailed)
    //         return;
    //
    //     Vector3 right = new Vector3(SPREAD_SHOT_OFFSET_X, 0, 0);
    //     
    //     Projectile rightBall = ProjectileCreator.CreateProjectile();
    //     rightBall.Initialize(right);
    //   
    //     
    //
    //     Vector3 left = new Vector3(-SPREAD_SHOT_OFFSET_X,0,0);
    //     Projectile leftBall = ProjectileCreator.CreateProjectile();
    //   
    //     leftBall.Initialize(left);
    // }
    //
    // private void EnableSpreadShot()
    // {
    //     if (IsSpreadShotEnabled)
    //         return;
    //     IsSpreadShotEnabled = true;
    //     
    // }
}
