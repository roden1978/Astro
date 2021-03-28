using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMD : ABullet
{
#pragma warning disable 0649
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] float lifetime;
    [SerializeField] float radius;
    [SerializeField] float distance;
    [SerializeField] WeaponSettings weapon;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649

    private IEnumerator coroutine;
    private Vector3 shootDirection;
    private bool isCollision;
    private RaycastHit2D[] hit;
    private AEnemy enemy;

    void Start()
    {
        damage = weapon.Damage;
    }

    private void OnEnable()
    {
        Invoke("Destroy", lifetime);
        if (weapon)
        {
            shootDirection = weapon.ShootPoint.DirectionTo(weapon.TargetPoint);
            rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
            hit = Physics2D.CircleCastAll(weapon.ShootPoint, radius, shootDirection, distance,
                1 << LayerMask.NameToLayer("Enemy"));
            
        }
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        isCollision = true;
        gameObject.SetActive(false);
        OnDestroy();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    
    public void OnDisable()
    {
        CancelInvoke();
    }

   
    private void OnDestroy()
    {
        if (isCollision)
        {
            foreach (var obj in hit)
            {
                if (obj.transform.gameObject.CompareTag("enemie"))
                {
                    Instantiate(vfxCollision, obj.collider.bounds.center, Quaternion.identity);
                    if(!enemy) enemy = obj.transform.gameObject.GetComponent<AEnemy>();
                    enemy.onDamage?.Invoke(damage);
                }
            }
        }

        isCollision = false;
    }
    
}