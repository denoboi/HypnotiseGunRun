using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyScale : MonoBehaviour
{
    private Money _money;
    private Money Money => _money == null ? _money = GetComponentInParent<Money>() : _money;

    [SerializeField] private Transform _body;

    private const float MIN_SCALE_MULTIPLIER = 0.01f;
    private const float SCALE_DURATION = 0.25f;
    private const Ease SCALE_TWEEN_EASE = Ease.InOutSine;

    private Vector3 _defaultScale;
    private string _scaleTweenID;

    private void Awake()
    {
        _defaultScale = _body.localScale;
        _scaleTweenID = GetInstanceID() + "ScaleTweenID";
    }

    private void OnEnable()
    {
        Money.OnInitialized.AddListener(ScaleUp);
    }

    private void OnDisable()
    {
        Money.OnInitialized.RemoveListener(ScaleUp);
    }

    private void ScaleUp() 
    {
        Vector3 from = _defaultScale * MIN_SCALE_MULTIPLIER;
        Vector3 to = _defaultScale;
        ScaleTween(from, to, SCALE_DURATION);
    }

    private void ScaleTween(Vector3 from, Vector3 to, float duration)
    {
        DOTween.Kill(_scaleTweenID);
        _body.localScale = from;
        _body.DOScale(to, duration).SetEase(SCALE_TWEEN_EASE).SetId(_scaleTweenID);
    }
}
