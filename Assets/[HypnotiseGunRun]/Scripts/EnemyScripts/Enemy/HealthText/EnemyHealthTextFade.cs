using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealthTextFade : MonoBehaviour
{
    private Enemy _enemy;
    private Enemy Enemy => _enemy ??= GetComponentInParent<Enemy>();

    [SerializeField] private TextMeshPro _textMesh;
    [SerializeField] private SpriteRenderer _backgroundRenderer;

    private const float FADE_DURATION = 0.35f;
    private const float MIN_FADE = 0f;

    private string _fadeTweenID;

    private void Awake()
    {
        _fadeTweenID = GetInstanceID() + "FadeTweenID";
    }

    private void OnEnable()
    {
        Enemy.OnKilled.AddListener(FadeOut);
    }

    private void OnDisable()
    {
        Enemy.OnKilled.RemoveListener(FadeOut);
    }

    private void FadeOut()
    {
        DOTween.Kill(_fadeTweenID);
        _textMesh.DOFade(MIN_FADE, FADE_DURATION).SetEase(Ease.Linear).SetId(_fadeTweenID);
        _backgroundRenderer.DOFade(MIN_FADE, FADE_DURATION).SetEase(Ease.Linear).SetId(_fadeTweenID);
    }
}
