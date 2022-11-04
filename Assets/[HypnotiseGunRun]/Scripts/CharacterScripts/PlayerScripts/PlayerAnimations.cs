using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Player _player;
    private Player Player => _player == null ? _player = GetComponentInParent<Player>() : _player;


    private Animator _animator;
    private Animator Animator => _animator == null ? _animator = GetComponentInParent<Animator>() : _animator;

    private const string JUMP_PARAMETER = "Jump";
    private const string ROLL_PARAMETER = "Roll";
    private const string RUN_PARAMETER = "Run";
    private const string SLIDE_PARAMETER = "IsSliding";
    private const string VICTORY_PARAMETER = "Victory";
    private const string FAIL_PARAMETER = "Fail";

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        GameManager.Instance.OnStageSuccess.AddListener(OnSuccess);
        LevelManager.Instance.OnLevelStart.AddListener(OnLevelStarted);
        HCB.Core.EventManager.OnPlayerFailed.AddListener(OnPlayerFailed);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        GameManager.Instance.OnStageSuccess.AddListener(OnSuccess);
        LevelManager.Instance.OnLevelStart.AddListener(OnLevelStarted);
        HCB.Core.EventManager.OnPlayerFailed.RemoveListener(OnPlayerFailed);
    }

    private void OnPlayerFailed()
    {
        SetTrigger(FAIL_PARAMETER);
    }

    private void OnLevelStarted()
    {
        SetTrigger(RUN_PARAMETER);
    }

    private void OnSuccess()
    {
        SetTrigger(VICTORY_PARAMETER);
    }


    private void SetTrigger(string parameter)
    {
        Animator.SetTrigger(parameter);
    }
}