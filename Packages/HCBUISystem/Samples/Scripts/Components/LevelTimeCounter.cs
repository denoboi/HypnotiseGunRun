using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HCB.Core;

namespace HCB.UI
{
    public class LevelTimeCounter : MonoBehaviour
    {
        Text counterText;
        Text CounterText { get { return (counterText == null) ? counterText = GetComponent<Text>() : counterText; } }

        private float startTime = 0;

        private void OnEnable()
        {
            LevelManager.Instance.OnLevelStart.AddListener(() => StartCoroutine(StartCounting()));
            LevelManager.Instance.OnLevelFinish.AddListener(() => StopCoroutine(StartCounting()));
        }

        private void OnDisable()
        {
            LevelManager.Instance.OnLevelStart.RemoveListener(()=> StartCoroutine(StartCounting()));
            LevelManager.Instance.OnLevelFinish.RemoveListener(() => StopCoroutine(StartCounting()));
        }

        private IEnumerator StartCounting()
        {
            startTime = Time.time;
            while (true)
            {
                float timePassed = Mathf.Abs(startTime - Time.time);
                CounterText.text = "Play Time: " + timePassed.ToString("f0");
                yield return null;
            }
        }
    }
}
