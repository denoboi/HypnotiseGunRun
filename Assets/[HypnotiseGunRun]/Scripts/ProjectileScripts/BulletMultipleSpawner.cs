using System;
using System.Collections;
using System.Collections.Generic;
using HCB.PoolingSystem;
using UnityEngine;

public class BulletMultipleSpawner : MonoBehaviour
{
   public List<GameObject> BulletList;
   private const string BULLET_ID = "Bullet";
   [SerializeField] private int _bulletCount;

   public Transform SpawnPoint;
   public GameObject BulletPrefab;

   private void Start()
   {
      SpawnBullet();
   }

   public void SpawnBullet()
   {
      
         StartCoroutine(BulletSpawnCo());
    
   }

   private void Spawn()
   {
      GameObject bullet = Instantiate(BulletPrefab, SpawnPoint.position, Quaternion.identity);
      bullet.GetComponentInChildren<Projectile>().Initialize(Vector3.forward);

      BulletList.Add(bullet);
      

   }

   IEnumerator BulletSpawnCo()
   {
      for (int i = 0; i < _bulletCount; i++)
      {
         Spawn();
         yield return new WaitForSeconds(.1f);
      }
     
     
   }

   


}