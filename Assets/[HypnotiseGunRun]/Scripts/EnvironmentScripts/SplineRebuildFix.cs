using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using Sirenix.OdinInspector;

public class SplineRebuildFix : MonoBehaviour
{
    private SplineComputer _splineComputer;
    private SplineComputer SplineComputer => _splineComputer == null ? _splineComputer = GetComponentInChildren<SplineComputer>() : _splineComputer;

    [Button]
    private void Rebuild()
    {
        if (SplineComputer == null)
            return;

        SplineComputer.RebuildImmediate();
    }

    private void OnValidate()
    {
        Rebuild();
    }
}
