using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HCB.Core;
using UnityEngine;
using UnityEngine.Serialization;

public class GroupController : MonoBehaviour
{
    public float speed = 1;
    public float SwipeSpeed;

    float distanceTravelled;
    public float startXOffset;
    float xOffset, yOffset;
    float maxDistance = 3f;

    public bool isLeft;
    public bool isLevelSuccess;
    bool isFinalWay;
    bool isLevelFail;
    public bool isWait;
    bool isSplit;

    //SnakeController
    // Settings
    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;

    [HideInInspector] public Transform LineHolder;

    // References
    public GameObject BodyPrefab;

    // Lists
    [FormerlySerializedAs("BodyParts")] [HideInInspector]
    public List<GameObject> ProjectileParts = new List<GameObject>();

    private List<Vector3> PositionsHistory = new List<Vector3>();

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;
    }

    private void Start()
    {
        GrowSnake();
    }

    private void Update()
    {
        if (!isLevelFail && !isWait && !isFinalWay)
        {
            //float h = InputManager.Instance.InputDirection.x;

            
        }
        else if (isFinalWay)
        {
        }
    }

    int stairNum = 0;
    bool oneTime = true;

    void JumpStairs()
    {
    }


    //Snake
    public void GrowSnake()
    {
        if (ProjectileParts.Count - 1 < 0)
        {
            GameObject body = Instantiate(BodyPrefab,
                new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity,
                LineHolder);

            LineHolder.GetChild(LineHolder.childCount - 1).gameObject.transform.DOScale(Vector3.one, 0.3f);
            ProjectileParts.Add(body);
        }

        else
        {
            GameObject body = Instantiate(BodyPrefab,
                new Vector3(transform.position.x, transform.position.y + 1,
                    ProjectileParts[ProjectileParts.Count - 1].transform.position.z), Quaternion.identity, LineHolder);
            LineHolder.GetChild(LineHolder.childCount - 1).gameObject.transform.DOScale(Vector3.one, 0.5f);
            ProjectileParts.Add(body);
        }
    }

    public void DecreaseSnake()
    {
        if (ProjectileParts.Count > 0)
        {
            GameObject particule = ProjectileParts[0].transform.GetChild(0).gameObject;
            ProjectileParts[0].transform.GetChild(0).parent = null;
            particule.SetActive(true);
            Destroy(ProjectileParts[0]);
            ProjectileParts.RemoveAt(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("MultiplicateDoor"))
        {
            StartCoroutine(MultiplacateMinion(other.gameObject));
        }
    }

    IEnumerator MultiplacateMinion(GameObject other)
    {
        // Debug.Log("mULTÝ");
        // int minionCount = transform.parent.GetChild(1).childCount;
        // for (int i = 0; i < (other.GetComponent<MultiplicationDoorController>().MultiplyNumber - 1) * minionCount; i++)
        // {
        //     GrowSnake();
        // }

        yield return new WaitForSeconds(0.1f);
    }
}