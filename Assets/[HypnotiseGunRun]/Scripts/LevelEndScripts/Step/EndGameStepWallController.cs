using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Experimental.SceneManagement;
#endif
using UnityEngine;

public class EndGameStepWallController : MonoBehaviour
{
    // [SerializeField] private EndGameWall _leftWall;
    // [SerializeField] private EndGameWall _rightWall;

    private EndGameStep _endGameStep;
    private EndGameStep EndGameStep => _endGameStep ??= GetComponentInParent<EndGameStep>(); 

    //Listens Editor Event
    // public void SetWall()
    // {
    //     if (EndGameStep.Multiplier % 2 == 0)
    //     {
    //         _leftWall.gameObject.SetActive(false);
    //         _rightWall.gameObject.SetActive(true);
    //
    //         _rightWall.Initialize(EndGameStep.Multiplier);
    //     }
    //
    //     else
    //     {
    //         _rightWall.gameObject.SetActive(false);
    //         _leftWall.gameObject.SetActive(true);
    //
    //         _leftWall.Initialize(EndGameStep.Multiplier);
    //     }
    // }    
}
