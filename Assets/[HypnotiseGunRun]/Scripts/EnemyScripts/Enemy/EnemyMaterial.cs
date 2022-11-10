using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaterial : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();

    private Renderer _renderer;
    private Renderer Renderer => _renderer ??= _visual.GetComponentInChildren<Renderer>();

    [SerializeField] private Transform _visual;
    [SerializeField] private Material _deadMaterial;    

    private void OnEnable()
    {
        Enemy.OnKilled.AddListener(SetDeadMaterial);
    }

    private void OnDisable()
    {
        Enemy.OnKilled.RemoveListener(SetDeadMaterial);
    }

    private void SetDeadMaterial() 
    {
        Renderer.material = _deadMaterial;
    }
}
