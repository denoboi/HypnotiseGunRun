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
    private const string UPGRADE_PARAMETER = "Upgrade";

    

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        GameManager.Instance.OnStageSuccess.AddListener(OnSuccess);
        LevelManager.Instance.OnLevelStart.AddListener(OnLevelStarted);
        HCB.Core.EventManager.OnPlayerFailed.AddListener(OnPlayerFailed);
        HCB.Core.EventManager.OnPlayerUpgraded.AddListener(OnUpgraded);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        GameManager.Instance.OnStageSuccess.RemoveListener(OnSuccess);
        LevelManager.Instance.OnLevelStart.RemoveListener(OnLevelStarted);
        HCB.Core.EventManager.OnPlayerFailed.RemoveListener(OnPlayerFailed);
        HCB.Core.EventManager.OnPlayerUpgraded.RemoveListener(OnUpgraded);

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

    private void OnUpgraded()
    {
        SetTrigger(UPGRADE_PARAMETER);
    }


    private void SetTrigger(string parameter)
    {
        Animator.SetTrigger(parameter);
    }
}