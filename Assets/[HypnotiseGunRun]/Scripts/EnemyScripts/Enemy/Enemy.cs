using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
  [Range(1, 50)]
  [SerializeField] private int _health;
  public int Health { get => _health; set => _health = value; }
  
  [HideInInspector]
  public UnityEvent OnHit = new UnityEvent();
  [HideInInspector]
  public UnityEvent OnKilled = new UnityEvent();
  
  
  
  
}
