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
    // Start is called before the first frame update
void Start()
    {
        firePoint = GameObject.Find("shootPoint").transform;
        targetPoint = GameObject.Find("targetPoint").transform;
        //rb.velocity = (targetPoint.transform.position - firePoint.transform.position) * 200f;
        rb.AddForce((targetPoint.transform.position - firePoint.transform.position) * 2f, ForceMode2D.Impulse);

        coroutine = Die();
        StartCoroutine(coroutine);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        Debug.Log(hitObject.name);
        Destroy(gameObject);
    }

    private void Update()
    {
       
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.2f);
    }

}
