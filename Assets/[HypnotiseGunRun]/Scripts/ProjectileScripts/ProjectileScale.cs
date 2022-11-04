using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ProjectileScale : MonoBehaviour
{
   private Projectile _projectile;

   public Projectile Projectile => _projectile == null ? _projectile = GetComponentInParent<Projectile>() : _projectile;
   private void OnEnable()
   {
      Projectile.OnInitialized.AddListener(ChangeScale);
   }

   private void OnDisable()
   {
      Projectile.OnInitialized.RemoveListener(ChangeScale);

   }

   void ChangeScale()
   {
      transform.DOScale(1f, 1f);
   }
}
