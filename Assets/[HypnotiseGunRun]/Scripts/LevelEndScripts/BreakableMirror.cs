using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BreakableMirror : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePieces;
    [SerializeField] private ParticleSystem _destructionParticle;

    private Transform _shatteredPiece;
    private Transform _normalPiece;
    
    
    private int _initialObstacleLevel = 12;

    [FormerlySerializedAs("ObstacleLevel")] public int MirrorLevel = 5;
    

    private bool _isDestructed;

    private BoxCollider _collider;
    public BoxCollider Collider => _collider == null ? _collider = GetComponentInChildren<BoxCollider>() : _collider;
    
    [HideInInspector]
    public UnityEvent OnHit = new UnityEvent();
    
    [HideInInspector]
    public UnityEvent OnMirrorDestroyed = new UnityEvent();
    
    

    private bool _isCollided { get; set; }

    private Vector3 _shrinkedVector;
    
    
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _normalPiece = transform.GetChild(0).GetChild(0);
        _shatteredPiece = transform.GetChild(0).GetChild(1);
        
    }


    private void Init()
    {
        _initialObstacleLevel = MirrorLevel;
        
        foreach (var obstacle in _obstaclePieces)
        {
            Rigidbody rb = obstacle.GetComponent<Rigidbody>();
            MeshCollider meshCollider = obstacle.GetComponent<MeshCollider>();

            if (rb != null)
            {
                rb.isKinematic = true;
                meshCollider.convex = true;

                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                meshCollider.isTrigger = true;
            }
            
          
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactor _player = other.GetComponentInChildren<Interactor>();

        if (_player != null)
        {
            Player.Instance.IsWin = true;
            GameManager.Instance.CompeleteStage(true);
        }
    }


    public void DestructMirror()
    {
        if (_isDestructed)
            return;
        
        _shatteredPiece.gameObject.SetActive(true);
        _normalPiece.gameObject.SetActive(false);
        _isDestructed = true;
        Collider.enabled = false;
        
        foreach (var obstacle in _obstaclePieces)
        {
            obstacle.GetComponent<Rigidbody>().isKinematic = false;
            obstacle.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2)) * 100);

            obstacle.GetComponent<MeshCollider>().isTrigger = false;
            _shrinkedVector = new Vector3(0.0001f, 0.0001f, 0.0001f);
            obstacle.transform.DOScale(_shrinkedVector, 1).SetDelay(.9f).OnComplete(() => {gameObject.SetActive(false);});
            
        }

        
      
        HapticManager.Haptic(HapticTypes.RigidImpact);
    }
    
    
    
}
