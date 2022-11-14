using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using UnityEngine;

public class ObstacleParticle : MonoBehaviour
{
    private const string PARTICLE_ID = "ObstacleParticle";

    private ObstacleDestruction _obstacleDestruction;
    
    public ObstacleDestruction ObstacleDestruction =>
        _obstacleDestruction == null
            ? _obstacleDestruction = GetComponent<ObstacleDestruction>()
            : _obstacleDestruction;

    private void OnEnable()
    {
        ObstacleDestruction.OnHit.AddListener(PlayParticle);
    }

    private void OnDisable()
    {
        ObstacleDestruction.OnHit.RemoveListener(PlayParticle);

    }

    private void PlayParticle()
    {
        PoolingSystem.Instance.InstantiateAPS(PARTICLE_ID, transform.position + Vector3.back / 2);
    }
}
