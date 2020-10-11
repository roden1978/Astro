using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField] Rigidbody2D rb;
	[SerializeField] float power;
	#pragma warning restore 0649
	
	private GameObject gun;
    private IEnumerator coroutine;
	private Vector3 shootDirection;
    // Start is called before the first frame update
void Start()
    {
	    gun = GameObject.Find("gun(Clone)");
	    if (gun != null){
		    shootDirection = gun.GetComponent<astroGun>().TargetPoint.position - gun.GetComponent<astroGun>().ShootPoint.position;
		    rb.AddForce(shootDirection * power, ForceMode2D.Impulse);
		    
		    Debug.Log("shootDirection " + shootDirection);
		    
		    coroutine = Die(1.5f);
		    StartCoroutine(coroutine);
	    }
	   
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        Debug.Log(hitObject.name);
        Destroy(gameObject);    
    }

    private void Update()
    {
       
    }

    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
