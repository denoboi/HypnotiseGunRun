using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HCB.EndGameSystem
{
    public class EndGameCreator : MonoBehaviour
    {
        [Range(1, 50)]
        [SerializeField] private int _stepCount;
        [Space(10)]
        [SerializeField] private GameObject _stepPrefab;      
        [SerializeField] private EndGameColorData _endGameColorData;
        [Space(10)]
        [SerializeField] private Transform _stepsParent;       
        [SerializeField] private Transform _platformEnd;        

        [ReadOnly]
        [SerializeField] private List<EndGameStep> _createdEndGameSteps = new List<EndGameStep>();

        [Button]
        private void Create()
        {
#if UNITY_EDITOR
            Destroy();

            List<Color> colors = new List<Color>(_endGameColorData.Colors);

            for (int i = 0; i < _stepCount; i++)
            {
                int multiplier = i + 1;

                int colorIndex = i % colors.Count;
                Color color = colors[colorIndex];

                EndGameStep endGameStep = (PrefabUtility.InstantiatePrefab(_stepPrefab) as GameObject).GetComponentInChildren<EndGameStep>();
                endGameStep.Initialize(multiplier, color);

                endGameStep.transform.SetParent(_stepsParent);
                endGameStep.transform.localPosition = endGameStep.GetBounds().size.z * i * Vector3.forward;

                _createdEndGameSteps.Add(endGameStep);
            }

            _platformEnd.transform.localPosition = _createdEndGameSteps.Last().GetBounds().size.z * _stepCount * Vector3.forward;
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
}
