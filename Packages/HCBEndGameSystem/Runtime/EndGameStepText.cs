using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif

namespace HCB.EndGameSystem
{
    public class EndGameStepText : MonoBehaviour
    {
        private EndGameStep _endGameStep;
        private EndGameStep EndGameStep => _endGameStep ??= GetComponentInParent<EndGameStep>();

        [SerializeField] TextMeshPro _multiplierTextMesh;

        private const string MULTIPLIER_SYMBOL = "x";

        private void Awake()
        {
            SetMultiplierText();
        }

        public void SetMultiplierText()
        {
            _multiplierTextMesh.SetText(MULTIPLIER_SYMBOL + EndGameStep.Multiplier);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (PrefabStageUtility.GetPrefabStage(gameObject) != null)
                return;
#endif
            if (EndGameStep == null || !EndGameStep.IsInitialized)
                return;

            SetMultiplierText();
        }
    }
}
