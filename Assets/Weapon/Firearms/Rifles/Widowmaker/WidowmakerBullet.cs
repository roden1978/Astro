using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidowmakerBullet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] float lifetime;
    [SerializeField] float radius;
    [SerializeField] float distance;
    [SerializeField] Weapon gun;
    [SerializeField] GameObject vfxCollision;
#pragma warning restore 0649

    //private GameObject gun;
    private IEnumerator coroutine;
    private Vector3 shootDirection;
    private bool isCollision;

    //private CapsuleCollider2D cc;

    private RaycastHit2D[] hit;

    // Start is called before the first frame update
    void Start()
    {
        //cc = transform.GetComponent<CapsuleCollider2D>();
        if (gun)
        {
            
            shootDirection = gun.TargetPoint - gun.ShootPoint;
            rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
            hit = Physics2D.CircleCastAll(gun.ShootPoint, radius, shootDirection, distance,
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
        gun.IsReady = true;
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