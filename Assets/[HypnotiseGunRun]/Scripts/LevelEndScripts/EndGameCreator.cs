using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;

public class EndGameCreator : MonoBehaviour
{
    [SerializeField] private int _stepCount;
    [Space(10)]
    [SerializeField] private Transform _endPlatform;
    [SerializeField] private Transform _stepsParent;
    [SerializeField] private GameObject _stepPrefab;    
    [Space(10)]
    [SerializeField] private List<Color> _colors;

    [ReadOnly]
    [SerializeField] private List<EndGameStep> _createdEndGameSteps = new List<EndGameStep>();

    [Button]
    private void Create() 
    {
#if UNITY_EDITOR
        Destroy();

        for (int i = 0; i < _stepCount; i++)
        {
            int multiplier = i + 1;

            int colorIndex = i % _colors.Count;
            Color color = _colors[colorIndex];

            EndGameStep endGameStep = (PrefabUtility.InstantiatePrefab(_stepPrefab) as GameObject).GetComponentInChildren<EndGameStep>();
            endGameStep.Initialize(multiplier, color);

            endGameStep.transform.SetParent(_stepsParent);
            endGameStep.transform.localPosition = endGameStep.GetBounds().size.z * i * Vector3.forward;

            _createdEndGameSteps.Add(endGameStep);
        }

        _endPlatform.transform.localPosition = _createdEndGameSteps.Last().GetBounds().size.z * _stepCount * Vector3.forward;
#endif
    }

    [Button]
    private void Destroy()
    {
        foreach (var endGameStep in _createdEndGameSteps)
        {
            if (endGameStep == null)
                continue;

            DestroyImmediate(endGameStep.gameObject);
        }
        _createdEndGameSteps.Clear();
    }
}
