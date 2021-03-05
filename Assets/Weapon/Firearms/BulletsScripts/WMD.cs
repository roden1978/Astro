﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMD : MonoBehaviour
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

    void Start()
    {
        if (weapon)
        {
            
            shootDirection = weapon.TargetPoint - weapon.ShootPoint;
            rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
            hit = Physics2D.CircleCastAll(weapon.ShootPoint, radius, shootDirection, distance,
                1 << LayerMask.NameToLayer("Enemy"));
            coroutine = Die(lifetime);
            StartCoroutine(coroutine);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        isCollision = true;
        Destroy(gameObject);
    }

    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

   
    private void OnDestroy()
    {
        weapon.IsReady = true;
        if (isCollision)
        {
            foreach (var obj in hit)
            {
                Debug.Log(obj.collider.bounds.center);
                Instantiate(vfxCollision, obj.collider.bounds.center, Quaternion.identity);
            }
        }

        isCollision = false;
    }
}