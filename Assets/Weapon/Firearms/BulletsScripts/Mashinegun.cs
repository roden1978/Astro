using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Mashinegun : ABullet
{
#pragma warning disable 0649
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] float bulletNoise;
    [SerializeField] float powerNoise;
    [SerializeField] WeaponSettings weapon;
    [SerializeField] GameObject vfxCollision;
    [SerializeField] float lifetime;
#pragma warning restore 0649
	
    private IEnumerator coroutine;
    private Vector3 shootDirection;
    private CapsuleCollider2D cc;
    private AEnemy enemy;
    
        void Start()
    {
        cc = transform.GetComponent<CapsuleCollider2D>();
        damage = weapon.Damage;
        showEffect.AddListener(Play);
    }

    private void OnEnable()
    {
        Invoke("Destroy", lifetime);
        if (weapon){
            shootDirection = (new Vector3(weapon.TargetPoint.x, weapon.TargetPoint.y + Random.Range(-bulletNoise, bulletNoise), weapon.TargetPoint.z)) - weapon.ShootPoint;
            rb.AddForce(shootDirection * (power + Random.Range(-powerNoise, powerNoise)), ForceMode2D.Impulse);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        showEffect?.Invoke();
        if (hitObject.gameObject.CompareTag("enemie"))
        {
            if(!enemy) enemy = hitObject.gameObject.GetComponent<AEnemy>();
            enemy.OnDamage?.Invoke(damage);
        }
            
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void Play()
    {
        Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
  
}
