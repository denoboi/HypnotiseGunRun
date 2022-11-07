using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using TMPro;
using UnityEngine;

public class ObstacleText : MonoBehaviour
{
    private TextMeshPro _text;
    public TextMeshPro Text => _text == null ? _text = GetComponentInChildren<TextMeshPro>() : _text;

    private ObstacleDestruction _obstacleDestruction;

    public ObstacleDestruction ObstacleDestruction => _obstacleDestruction == null
        ? _obstacleDestruction = GetComponentInParent<ObstacleDestruction>()
        : _obstacleDestruction;


    private void OnEnable()
    {
        SceneController.Instance.OnSceneLoaded.AddListener(ShowText);
        ObstacleDestruction.OnHit.AddListener(ShowText);
    }
   
    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
        SceneController.Instance.OnSceneLoaded.RemoveListener(ShowText);
        ObstacleDestruction.OnHit.AddListener(ShowText);

    }

    void ShowText()
    {
        Text.text = ObstacleDestruction.ObstacleLevel.ToString();

        if (ObstacleDestruction.ObstacleLevel <= 0)
        {
            Text.text = null;
        }
    }
    
}
