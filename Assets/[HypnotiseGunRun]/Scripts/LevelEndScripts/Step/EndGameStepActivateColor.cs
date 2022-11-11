using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGameStepActivateColor : MonoBehaviour
{
    private EndGameStep _endGameStep;
    private EndGameStep EndGameStep => _endGameStep ??= GetComponentInParent<EndGameStep>();

    [SerializeField] private Color _activateColor;
    [SerializeField] private Renderer _bodyRenderer;

    private const float COLOR_DURATION = 0.35f;
    private const Ease COLOR_TWEEN_EASE = Ease.InOutSine;

    private Color _defaultColor;
    private string _activateColorTweenID;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake()
    {
        SetupPropertyBlock();
        _activateColorTweenID = GetInstanceID() + "ActivateColorTweenID";
    }

    private void OnEnable()
    {
        EndGameStep.OnActivated.AddListener(ActivateColorTween);
    }

    private void OnDisable()
    {
        EndGameStep.OnActivated.RemoveListener(ActivateColorTween);
    }
   
    private void ActivateColorTween() 
    {
        DOTween.Kill(_activateColorTweenID);
        Sequence activateColorSeq = DOTween.Sequence();
        activateColorSeq
        .Append(ColorTween(_defaultColor, _activateColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_activateColor, _defaultColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_defaultColor, _activateColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_activateColor, _defaultColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_defaultColor, _activateColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_activateColor, _defaultColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_defaultColor, _activateColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .Append(ColorTween(_activateColor, _defaultColor, COLOR_DURATION, COLOR_TWEEN_EASE, _activateColorTweenID))
        .SetId(_activateColorTweenID);
    }

    private Tween ColorTween(Color startValue, Color endValue, float duration, Ease ease, string ID) 
    {
        Tween tween = DOTween.To(() => startValue, (x) => startValue = x, endValue, duration).SetEase(ease).SetId(ID).OnUpdate(() =>
        {
            SetColor(startValue);
        });
        return tween;
    }

    private void SetColor(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        _bodyRenderer.SetPropertyBlock(_propertyBlock);
    }

    private void SetupPropertyBlock() 
    {
        _defaultColor = EndGameStep.Color;
        _propertyBlock = new MaterialPropertyBlock();
        _propertyBlock.SetColor("_Color", _defaultColor);       
    }
}
