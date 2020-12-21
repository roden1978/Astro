using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulbreakerBullet : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float power;
    [SerializeField] Weapon gun;
    [SerializeField] GameObject vfxCollision;
    [SerializeField] float lifetime;
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
            shootDirection = gun.TargetPoint - (new Vector3(gun.ShootPoint.x, gun.ShootPoint.y + Random.Range(-0.05f, 0.05f), gun.ShootPoint.z));
            rb.AddForce(shootDirection * (power + Random.Range(-0.1f, 0.1f)), ForceMode2D.Impulse);
		    
            Debug.Log("shootDirection " + shootDirection);
		    
            coroutine = Die(lifetime);
            StartCoroutine(coroutine);
        }
	    
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        if(!hitObject.CompareTag("playerBullet"))
        {
            Instantiate(vfxCollision, cc.transform.position, Quaternion.identity);
            Destroy(gameObject);
        } 
    }
    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
