using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour
{
    public Transform Target;
    public bool IsFollow;

    private void Update()
    {
        if (IsFollow && Target != null)
            transform.DOLocalMove(Target.localPosition, 0.3f);
    }
}
