using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    private Animator _animator;
    private static readonly int GunFire = Animator.StringToHash("GunFire");
    private Animator Animator => _animator == null ? _animator = GetComponentInParent<Animator>() : _animator;
    
    private void OnEnable()
    {
        HCB.Core.EventManager.OnShoot.AddListener(FireAnim);
    }

    private void OnDisable()
    {
        HCB.Core.EventManager.OnShoot.RemoveListener(FireAnim);
    }

    private void FireAnim()
    {
        Animator.SetTrigger(GunFire);
    }
}
