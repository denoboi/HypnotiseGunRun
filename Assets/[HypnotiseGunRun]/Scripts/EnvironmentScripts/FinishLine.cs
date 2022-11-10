using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static FinishLine Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsEndGame { get; private set; }
    private void OnTriggerEnter(Collider other)
    {
        Interactor player = other.GetComponentInParent<Interactor>();

        PlayerAnimations playerAnimations = other.GetComponentInParent<PlayerAnimations>();

        if (player != null)
        {
           
            GameManager.Instance.CompeleteStage(true);
            // HCB.Core.EventManager.OnEnteredEndGame.Invoke();
            IsEndGame = true;
            Player.Instance.IsWin = true;

        }
    }
}
