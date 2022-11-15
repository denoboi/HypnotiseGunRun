using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTargetDurabilities : MonoBehaviour
{
    
    
    int[] durabilities = { 10,10, 20,20, 50,50, 100,100, 200,200, 300,300, 400,400, 500,500, 600,600, 700,700, 800,800, 900,900, 1000,1000 };
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        SetTargetDurabilities();
    }

    void SetTargetDurabilities()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int currentTargetDurability;
            if (i > durabilities.Length - 1)
            {
                currentTargetDurability = durabilities.Length - 1;
            }
            else
            {
                currentTargetDurability = i;
            }
            transform.GetChild(i).GetComponentInChildren<MirrorText>().SetDurability(durabilities[currentTargetDurability]);
            //transform.GetChild(i+1).GetComponentInChildren<MirrorText>().SetDurability(durabilities[currentTargetDurability]);



        }
    }
}
