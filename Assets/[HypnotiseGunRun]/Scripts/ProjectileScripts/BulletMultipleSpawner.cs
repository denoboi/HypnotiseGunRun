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
      Spawn();
   }

  
   private void Spawn()
   {
      
      for (int i = 0; i < _bulletCount; i++)
      {
         GameObject newBullet= Instantiate(BulletPrefab, SpawnPoint.position, Quaternion.identity);
         newBullet.GetComponent<Projectile>().Initialize(Vector3.forward);
         
         
         if(BulletList.Count>=1)
         {
            newBullet.transform.position = new Vector3(transform.position.x, transform.position.y,
               BulletList[BulletList.Count - 1].transform.position.z - .5f);
            
            newBullet.GetComponentInChildren<ProjectileFollow>().Target = BulletList[BulletList.Count - 1].transform;
            newBullet.GetComponentInChildren<ProjectileFollow>().IsFollow = true;
            //newBullet.GetComponentInChildren<Rigidbody>().isKinematic = true;
            //newBullet.GetComponentInChildren<Collider>().isTrigger = true;

         }
         
         BulletList.Add(newBullet);

      } 
      
      
     

   }

   
     
     
}

