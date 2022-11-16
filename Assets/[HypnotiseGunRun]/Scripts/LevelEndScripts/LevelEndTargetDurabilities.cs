using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndTargetDurabilities : MonoBehaviour
{
    
    
    int[] durabilities = { 10,10, 20,20, 30,30, 40,40, 50,50, 60,60, 70,70, 80,80, 90,90, 100,100 };
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

            Transform textParent = transform.GetChild(i).GetComponentInChildren<MirrorText>().transform.parent;
            
            textParent.GetComponentsInChildren<MirrorText>()[0].SetDurability(durabilities[currentTargetDurability]);
            textParent.GetComponentsInChildren<MirrorText>()[1].SetDurability(durabilities[currentTargetDurability]);
            //transform.GetChild(i+1).GetComponentInChildren<MirrorText>().SetDurability(durabilities[currentTargetDurability]);



        }
    }
}
