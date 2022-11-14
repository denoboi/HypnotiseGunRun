using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyTrail : MonoBehaviour
{
    private TrailRenderer _trailRenderer;
    private TrailRenderer TrailRenderer => _trailRenderer == null ? _trailRenderer = GetComponentInChildren<TrailRenderer>() : _trailRenderer;

    private Money _money;
    private Money Money => _money == null ? _money = GetComponentInParent<Money>() : _money; 

    private void OnEnable()
    {
        Money.OnCollected.AddListener(EnableTrail);
        Money.OnInitialized.AddListener(DisableTrail);
    }

    private void OnDisable()
    {
        Money.OnCollected.RemoveListener(EnableTrail);
        Money.OnInitialized.RemoveListener(DisableTrail);
    }

    private void EnableTrail()
    {
        TrailRenderer.emitting = true;
    }

    private void DisableTrail()
    {
        TrailRenderer.emitting = false;
    }
}
