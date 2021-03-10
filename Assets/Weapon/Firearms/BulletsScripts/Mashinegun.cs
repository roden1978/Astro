using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashinegun : MonoBehaviour
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
    void Start()
    {
        cc = transform.GetComponent<CapsuleCollider2D>();
        if (weapon){
            shootDirection = (new Vector3(weapon.TargetPoint.x, weapon.TargetPoint.y + Random.Range(-bulletNoise, bulletNoise), weapon.TargetPoint.z)) - weapon.ShootPoint;
            rb.AddForce(shootDirection * (power + Random.Range(-powerNoise, powerNoise)), ForceMode2D.Impulse);
            coroutine = Die(lifetime);
            StartCoroutine(coroutine);
        }
	    
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.CompareTag("playerBullet")) return;
        Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
