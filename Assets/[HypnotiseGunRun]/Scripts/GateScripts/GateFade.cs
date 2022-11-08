using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GateFade : MonoBehaviour
{
   public bool IsFadeOut { get; private set; }

    private GateBase _gate;
    private GateBase Gate => _gate == null ? _gate = GetComponentInParent<GateBase>() : _gate;

    private Renderer _visualRenderer;
    private Renderer VisualRenderer => _visualRenderer == null ? _visualRenderer = _visual.GetComponentInChildren<Renderer>() : _visualRenderer;

    private SpriteRenderer[] _spriteRenderers;
    private SpriteRenderer[] SpriteRenderers => _spriteRenderers == null ? _spriteRenderers = GetComponentsInChildren<SpriteRenderer>() : _spriteRenderers;

    private TextMeshPro[] _textMeshes;
    private TextMeshPro[] TextMeshes => _textMeshes == null ? _textMeshes = GetComponentsInChildren<TextMeshPro>() : _textMeshes;

    [SerializeField] private Transform _visual;

    private const float FADE_DURATION = 0.3f;
    private const float INTERACTED_FADE = 0f;

    private MaterialPropertyBlock _propertyBlock;
    private string _fadeTweenID;
    private float _currentAlpha = 1;

    private void Awake()
    {
        SetupPropertyBlock();
        _fadeTweenID = GetInstanceID() + "FadeTweenID";
    }

    private void OnEnable()
    {
        if (Gate == null)
            return;
        Gate.OnInteracted.AddListener(() => FadeOut(INTERACTED_FADE));
    }

    private void OnDisable()
    {
        if (Gate == null)
            return;
        Gate.OnInteracted.RemoveListener(() => FadeOut(INTERACTED_FADE));
    }    

    public void FadeOut(float alpha) 
    {
        if (IsFadeOut)
            return;

        IsFadeOut = true;
        SetupFadeOut();
        FadeTween(alpha);
    }    

    private void FadeTween(float endValue) 
    {        
        DOTween.To(() => _currentAlpha, (x) => _currentAlpha = x, endValue, FADE_DURATION).SetId(_fadeTweenID).SetEase(Ease.Linear).OnUpdate(() =>
        {
            SetSpriteRendererAlpha(_currentAlpha);
            SetTextMeshAlpha(_currentAlpha);
            SetVisualRendererAlpha(_currentAlpha);
        });
    }

    private void SetSpriteRendererAlpha(float alpha) 
    {
        foreach (var spriteRenderer in SpriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }

    private void SetTextMeshAlpha(float alpha) 
    {
        foreach (var testMesh in TextMeshes)
        {
            Color color = testMesh.color;
            color.a = alpha;
            testMesh.color = color;
        }
    }

    private void SetVisualRendererAlpha(float alpha) 
    {
        Color color = _propertyBlock.GetColor("_Color"); //material degistirirken perf artirmak icin
        color.a = alpha;

        _propertyBlock.SetColor("_Color", color);
        VisualRenderer.SetPropertyBlock(_propertyBlock);
    }

    private void SetupFadeOut()
    {
        VisualRenderer.receiveShadows = false;
        VisualRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    private void SetupPropertyBlock() 
    {
        _propertyBlock = new MaterialPropertyBlock();
        Color color = VisualRenderer.material.GetColor("_Color");
        _propertyBlock.SetColor("_Color", color);
        VisualRenderer.SetPropertyBlock(_propertyBlock);
    }
}
