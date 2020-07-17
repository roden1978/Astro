using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] Vector2 speed;
    [SerializeField] Rigidbody2D rb;
    Transform firePoint;
    // Start is called before the first frame update
void Start()
    {
        firePoint = GameObject.Find("shootPoint").transform;
        //print(Camera.main.ScreenToWorldPoint(firePoint.transform.posi));
        //speed = new Vector2(0.5f, 0.5f);
        //print(firePoint.transform.eulerAngles);
        rb.velocity = Camera.main.ScreenToWorldPoint(firePoint.transform.position);
        //rb.AddForce(new Vector2 (transform.localPosition.x, 0) * 0.1f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D hitObject)
    {
        Debug.Log(hitObject.name);
        Destroy(gameObject);
    }

    
}
