using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunchScale : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();

    [SerializeField] private Transform _punchBody;

    private const float PUNCH_STRENGTH = 0.15f;
    private const float PUNCH_DURATION = 0.1f;
    private const Ease PUNCH_EASE = Ease.InOutSine;

    private string _punchTweenID;

    private void Awake()
    {
        _punchTweenID = GetInstanceID() + "PunchTweenID";
    }

    private void OnEnable()
    {
        Enemy.OnHit.AddListener(PunchTween);
    }

    private void OnDisable()
    {
        Enemy.OnHit.RemoveListener(PunchTween);
    }

    private void PunchTween()
    {
        DOTween.Complete(_punchTweenID);
        _punchBody.DOPunchScale(Vector3.one * PUNCH_STRENGTH, PUNCH_DURATION, vibrato: 1).SetEase(PUNCH_EASE).SetId(_punchTweenID);
    }
}
