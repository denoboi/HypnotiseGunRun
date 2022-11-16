using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadShot : MonoBehaviour
{
    private PlayerProjectileCreator _playerProjectileCreator;
    
    
    public PlayerProjectileCreator PlayerProjectileCreator
    {
        get
        {
            return _playerProjectileCreator == null
                ? _playerProjectileCreator = GetComponentInChildren<PlayerProjectileCreator>()
                : _playerProjectileCreator;
        }
    }
    
    [SerializeField] private Vector3 _rightBallOffset = new Vector3(55, 0, 0);
    [SerializeField] private Vector3 _leftBallOffset = new Vector3(0, 0, 0);
    
    
    public bool IsSpreadShotEnabled { get; private set; }
    
    private void OnEnable()
    {
        HCB.Core.EventManager.OnSpreadShotGateInteracted.AddListener(EnableSpreadShot);
    }
    
    private void OnDisable()
    {
        HCB.Core.EventManager.OnSpreadShotGateInteracted.RemoveListener(EnableSpreadShot);
    }
    
    public void SpreadShotSpawn()
    {
        if (Player.Instance.IsFailed)
            return;
     
        Projectile rightBall = PlayerProjectileCreator.CreateProjectile();
        rightBall.Initialize(_rightBallOffset);
        
  
        Projectile leftBall = PlayerProjectileCreator.CreateProjectile();
        
        leftBall.Initialize(_leftBallOffset);
        
        
    }
    
    private void EnableSpreadShot()
    {
        if (IsSpreadShotEnabled)
            return;
        IsSpreadShotEnabled = true;
        
    }
}
