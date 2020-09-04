using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] Vector2 speed;
    [SerializeField] Rigidbody2D rb;
    Transform firePoint;
    Transform targetPoint;
    private IEnumerator coroutine;
    Vector3 shootDirection;
    // Start is called before the first frame update
void Start()
    {
        firePoint = GameObject.Find("shootPoint").transform;
        targetPoint = GameObject.Find("targetPoint").transform;
        //rb.velocity = (targetPoint.transform.position - firePoint.transform.position) * 200f;
        shootDirection = targetPoint.transform.position - firePoint.transform.position;
        rb.AddForce(shootDirection * 2.0f, ForceMode2D.Impulse);

        print(shootDirection);

        coroutine = Die(0.5f);
        StartCoroutine(coroutine);

        //Destroy(gameObject);
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
