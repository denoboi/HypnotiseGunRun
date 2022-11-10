using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class ObstacleDestruction : MonoBehaviour, IBreakable
{
    [SerializeField] private GameObject[] _obstaclePieces;
    [SerializeField] private ParticleSystem _destructionParticle;

    private int _initialObstacleLevel = 12;

    public int ObstacleLevel = 5;


    private bool _isDestructed;

    private BoxCollider _collider;
    public BoxCollider Collider => _collider == null ? _collider = GetComponentInChildren<BoxCollider>() : _collider;
    
    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();
    
    [HideInInspector]
    public UnityEvent OnObstacleDestroyed = new UnityEvent();
    
    [HideInInspector]
    public UnityEvent OnBigObstacleDestroyed = new UnityEvent();

    private bool _isCollided { get; set; }

    private Vector3 _shrinkedVector;
    
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _initialObstacleLevel = ObstacleLevel;
        
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.AddComponent<Rigidbody>().isKinematic = true;
            obstacle.AddComponent<MeshCollider>().convex = true;
            

            obstacle.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            obstacle.GetComponent<MeshCollider>().isTrigger = true;
        }
    }

   
    public void DestructObstacle()
    {
        if (_isDestructed)
            return;
        
        if (_initialObstacleLevel < 10)
        {
            OnObstacleDestroyed.Invoke();
            
        }

        if (_initialObstacleLevel >= 10)
        {
            OnBigObstacleDestroyed.Invoke();
        }
        
        
        _isDestructed = true;
        Collider.enabled = false;
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.GetComponent<Rigidbody>().isKinematic = false;
            obstacle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * 300);

            obstacle.GetComponent<MeshCollider>().isTrigger = false;
            _shrinkedVector = new Vector3(0.001f, 0.0001f, 0.0001f);
            obstacle.transform.DOScale(_shrinkedVector, 1).SetDelay(.8f).OnComplete(() => {obstacle.gameObject.SetActive(false);});
            
        }

        
      
        HapticManager.Haptic(HapticTypes.RigidImpact);
    }
    
    
}
