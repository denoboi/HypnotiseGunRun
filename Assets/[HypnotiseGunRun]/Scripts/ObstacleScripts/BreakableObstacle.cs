using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HCB.Core;
using Sirenix.OdinInspector;

public class BreakableObstacle : MonoBehaviour
{
    public bool isTurning;
    public bool isMoving;
    public List<GameObject> parts;
    [ShowIf("isMoving", true)]
    public List<Transform> movePosses;
    private int indexPos;

    public int ObstacleLevel = 88;
    private void Start()
    {
        if (isTurning)
        {
            transform.DORotate(360 * Vector3.up, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }

        if (isMoving)
        {

            for (int i = 0; i < parts.Count - 1; i++)
            {
                parts[i].transform.DORotate(360 * Vector3.forward, 4, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

            }
            MoveLeftAndRight();
        }
    }
    private void MoveLeftAndRight()
    {
        indexPos++;

        if (indexPos == 2)
            indexPos = 0;

        for (int i = 0; i < parts.Count - 1; i++)
        {
            if (i == 4)
                parts[i].transform.DOMoveX(movePosses[indexPos].position.x, 1f).OnComplete(MoveLeftAndRight);
            else
                parts[i].transform.DOMoveX(movePosses[indexPos].position.x, 1f);

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Interactor interactor = other.GetComponentInParent<Interactor>();
        Projectile projectile = other.GetComponentInParent<Projectile>();
        
        
        if (interactor != null)
        {
            HCB.Core.EventManager.OnPlayerFailed.Invoke();
            HapticManager.Haptic(HapticTypes.Failure);
            Player.Instance.IsFailed = true;
            
            Run.After(1,()=>GameManager.Instance.CompeleteStage(false));
            
            GetComponent<Collider>().enabled = false;
            Break();

        }
        
        else if (projectile != null)
        {
            ObstacleLevel--;
            HapticManager.Haptic(HapticTypes.SoftImpact);
            if (ObstacleLevel <= 0)
            {
                Break();
                HapticManager.Haptic(HapticTypes.RigidImpact);
                GetComponent<Collider>().enabled = false;
            }
            
        }
    }

    public void Break()
    {


        if (isMoving)
            return;
        DOTween.Kill(transform);

        for (int i = 0; i < parts.Count; i++)
        {

            parts[i].GetComponent<Collider>().enabled = true;

            parts[i].transform.DOScale(Vector3.one * 0.01f, 1.5f);
        }
    }
}
