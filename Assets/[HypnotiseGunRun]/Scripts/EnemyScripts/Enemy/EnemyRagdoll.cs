using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRagdoll : MonoBehaviour
{
    private RagdollController _ragdollController;

    public RagdollController RagdollController => _ragdollController == null
        ? _ragdollController = GetComponent<RagdollController>()
        : _ragdollController;

    private Enemy _enemy;
    public Enemy Enemy => _enemy == null ? _enemy = GetComponent<Enemy>() : _enemy;

    private void OnEnable()
    {
       Enemy.OnKilled.AddListener(EnableRagdoll);
    }

    private void OnDisable()
    {
        Enemy.OnKilled.RemoveListener(EnableRagdoll);
    }

    void EnableRagdoll()
    {
        RagdollController.EnableRagdollWithForce(new Vector3(Random.Range(0.1f, 0.3f), Random.Range(0.1f, 0.4f), Random.Range(0.3f, 1f)), Random.Range(300f, 700f), ForceMode.Force);
    }
}
