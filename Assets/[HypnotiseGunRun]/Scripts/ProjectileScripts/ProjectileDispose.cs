using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.XR;

public class ProjectileDispose : MonoBehaviour
{
    private Renderer[] _renderers;
    private Renderer[] Renderers => _renderers ??= GetComponentsInChildren<Renderer>();

    private Projectile _projectile;
    private Projectile Projectile => _projectile ??= GetComponentInParent<Projectile>();  

    private const float FADE_DURATION = .3f;
    private const float FADE_DELAY = 2f;
    private const float MIN_FADE = 0f;

    private string _fadeTweenID;

    private void Awake()
    {
        _fadeTweenID = GetInstanceID() + "FadeTweenID";
    }

    private void OnEnable()
    {
        Projectile.OnKilled.AddListener(DisposeEffect);
    }

    private void OnDisable()
    {
        Projectile.OnKilled.RemoveListener(DisposeEffect);
    }

    private void DisposeEffect()
    {
        float currentAlpha = 1f;
        DOTween.Kill(_fadeTweenID);
        DOTween.To(() => currentAlpha, (x) => currentAlpha = x, MIN_FADE, FADE_DURATION).OnStart(SetupDispose).SetDelay(FADE_DELAY).SetEase(Ease.Linear).SetId(_fadeTweenID).OnUpdate(() =>
        {
            Debug.Log(currentAlpha);
            foreach (var renderer in Renderers)
            {
                SetAlpha(renderer, currentAlpha);
            }
        }).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });        
    }

    private void SetAlpha(Renderer renderer, float alpha) 
    {
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }

    private void SetupDispose()
    {
        foreach (var renderer in Renderers)
        {
            renderer.receiveShadows = false;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }        
    }
}
