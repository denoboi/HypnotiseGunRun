using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif

namespace HCB.EndGameSystem
{
    public class EndGameStepVisual : MonoBehaviour
    {
        private EndGameStep _endGameStep;
        private EndGameStep EndGameStep => _endGameStep ??= GetComponentInParent<EndGameStep>();

        private MaterialPropertyBlock _materialPropertyBlock;

        private void Awake()
        {
            SetColor();
        }

        public void SetColor()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            _materialPropertyBlock.SetColor("_Color", EndGameStep.Color);
            EndGameStep.Renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (PrefabStageUtility.GetPrefabStage(gameObject) != null)
                return;
#endif
            if (EndGameStep == null || !EndGameStep.IsInitialized)
                return;

            SetColor();
        }
    }
}
