using System;
using System.Collections;
using System.Collections.Generic;
using HCB.Core;
using UnityEngine;

public class TreasureFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Player.Instance.IsWin = true;
        GameManager.Instance.CompeleteStage(true);
    }
}
