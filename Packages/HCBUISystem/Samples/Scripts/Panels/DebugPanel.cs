using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;
using TMPro;

namespace HCB.UI
{
    public class DebugPanel : HCBPanel
    {

        public Text TimeScaleText;

        private void OnEnable()
        {
            if (Managers.Instance == null)
                return;

           InputManager.Instance.OnTouch.AddListener(ToogleOnCount);
        }

        private void OnDisable()
        {
            if (Managers.Instance == null)
                return;

            InputManager.Instance.OnTouch.AddListener(ToogleOnCount);
        }

        public void ToogleOnCount(int value)
        {
            if (value == 3)
                TooglePanel();
        }

        private void TooglePanel()
        {
            if (CanvasGroup.alpha == 1)
                HidePanel();
            else ShowPanel();
        }

        public void LoadNextLevel()
        {
            LevelManager.Instance.LoadNextLevel();
        }

        public void LoadPreviousLevel()
        {
            LevelManager.Instance.LoadPreviousLevel();
        }

        public void RestartLevel()
        {
            LevelManager.Instance.ReloadLevel();
        }

        public void CompilateStage(bool value)
        {
            GameManager.Instance.CompeleteStage(value);
        }

        public void IncreaseTimeScale()
        {
            Time.timeScale += 0.5f;
            TimeScaleText.text = Time.timeScale.ToString();
        }

        public void DecreaseTimeScale()
        {
            Time.timeScale -= 0.5f;
            TimeScaleText.text = Time.timeScale.ToString();
        }

        public void ResetTime()
        {

            Time.timeScale = 1f;
            TimeScaleText.text = Time.timeScale.ToString();
        }
    }
}
