﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightstoneBullet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] Weapon gun;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649
	
    //private GameObject gun;
    private IEnumerator coroutine;
    private Vector3 shootDirection;

    private CapsuleCollider2D cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CapsuleCollider2D>();
        if (gun){
            shootDirection = gun.TargetPoint - gun.ShootPoint;
            rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
		    
            Debug.Log("shootDirection " + shootDirection);
		    
            coroutine = Die(1.0f);
            StartCoroutine(coroutine);
        }
	    
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
        Destroy(gameObject);    
    }
    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}