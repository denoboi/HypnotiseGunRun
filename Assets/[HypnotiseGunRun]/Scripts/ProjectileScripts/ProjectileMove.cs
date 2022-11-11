using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using HCB.Core;
using HCB.PoolingSystem;
using UnityEngine;
using Sirenix.OdinInspector;

public class ProjectileMove : MonoBehaviour
{
    public List<Transform> Projectiles = new List<Transform>();


    private Projectile _projectile;

    public Projectile Projectile
    {
        get { return _projectile == null ? _projectile = GetComponentInChildren<Projectile>() : _projectile; }
    }

    private Rigidbody _rb;

    public Rigidbody Rigidbody => _rb == null ? _rb = GetComponentInChildren<Rigidbody>() : _rb;

    private int _index = 0;
    public GameObject Ball;

    [SerializeField] float lerpTime;


    private int _ballCount;

    public Vector3 Offset;


    private bool _canShoot = true;

    //[SerializeField] private float _speed = 20f;
    

    private void OnEnable()
    {
        if (Projectile == null || PlayerFireRate.Instance == null)
            return;
        Projectile.OnInitialized.AddListener(MoveProjectile);


        _ballCount = PlayerFireRate.Instance.FireRate;

        if (Ball.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        Ball.transform.localPosition = Vector3.zero;

        for (int i = 0; i < _ballCount; i++)
        {
            GameObject ball = Instantiate(Ball, transform);

            ball.transform.localScale = Vector3.zero;
            ball.transform.DOScale(Vector3.one * .3f, 0.01f).SetDelay(i * 0.1f).SetEase(Ease.Linear);

            ball.GetComponentInChildren<MeshRenderer>().enabled = true;

            if (ball.TryGetComponent(out Rigidbody rigidbody))
            {
                Destroy(rigidbody);
            }

            ball.transform.position = new Vector3(Player.Instance.transform.GetChild(0).position.x, transform.position.y,transform.position.z);
           

            Projectiles.Add(ball.transform);
        }

        
        DestroyProjectilesCo();
    }

    private void OnDisable()
    {
        if (Projectile == null)
            return;
        Projectile.OnInitialized.RemoveListener(MoveProjectile);
        for (int i = 0; i < Projectiles.Count; i++)
        {
            if (i > 0)
                Destroy(Projectiles[i].gameObject);
        }

        Projectiles.Clear();

        Projectiles.Add(Ball.transform);

        _index = 0;
        
       
    }


    private void FixedUpdate()
    {
        if (Projectiles.Count <= 0)
            return;

        for (int i = 0; i < Projectiles.Count; i++)
        {
            if (i - 1 <= 0)
            {
                _index = 1;
            }
            else
            {
                _index = i;
            }


            Projectiles[_index].transform.position = new Vector3(Mathf.Lerp(Projectiles[_index].transform.position.x,
                Projectiles[_index - 1].transform.position.x + Offset.x, lerpTime * Time.deltaTime),
                Mathf.Lerp(Projectiles[_index].transform.position.y,
                    Projectiles[_index - 1].transform.position.y + Offset.y, lerpTime * Time.deltaTime),
                Mathf.Lerp(Projectiles[_index].transform.position.z,
                    Projectiles[_index - 1].transform.position.z + Offset.z, lerpTime * Time.deltaTime));
        }
    }

    [Button]
    private void MoveProjectile()
    {
        //if (!_canShoot) return;
        Rigidbody.AddForce(Projectile.Direction, ForceMode.Impulse);
        

        //transform.DOJump(new Vector3(transform.position.x, transform.position.y, transform.position.z +5) , 2f, 5, 5f).SetLoops(-1);
        //transform.Translate(Projectile.Direction * _speed * Time.deltaTime);
    }

    private IEnumerator DestroyProjectiles()
    {
        yield return new WaitForSeconds(PlayerFireRange.Instance.DestroyTime);
        for (int i = 0; i < transform.childCount; i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.GetChild(i).GetComponent<Renderer>().enabled = false;
            transform.GetChild(i).GetComponent<Collider>().enabled = false;
        }
    
       
    }
    
    private void DestroyProjectilesCo()
    {
        StartCoroutine(DestroyProjectiles());
    }
}