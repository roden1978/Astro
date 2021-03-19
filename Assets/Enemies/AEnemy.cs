using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AEnemy : MonoBehaviour
{
   protected int health;
   public UnityEvent<int> OnDamage;
   
   public abstract void TakeDamage(int damage);

   protected void Awake()
   {
      OnDamage = new UnityEvent<int>();
   }
}
