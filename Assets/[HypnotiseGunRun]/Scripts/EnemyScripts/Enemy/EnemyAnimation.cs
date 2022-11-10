using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator Animator => _animator ??= GetComponentInChildren<Animator>();

    private Enemy _enemy;
    private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();

    private const string HIT_PARAMETER = "Hit";
    private const string PLAYER_DEAD = "IsPlayerDead";

    private void OnEnable()
    {
        Enemy.OnHit.AddListener(() => SetTrigger(HIT_PARAMETER));
        HCB.Core.EventManager.OnPlayerFailed.AddListener(() => SetTrigger(PLAYER_DEAD));
    }

    private void OnDisable() 
    {
        Enemy.OnHit.RemoveListener(() => SetTrigger(HIT_PARAMETER));
        HCB.Core.EventManager.OnPlayerFailed.RemoveListener(() => SetTrigger(PLAYER_DEAD));
    }

    private void SetTrigger(string parameter) 
    {
        Animator.SetTrigger(parameter);
    }
}
