using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroGun : MonoBehaviour
{
    //private int type = 0;
    [SerializeField] Transform firePoint; // shoot point
    [SerializeField] Transform targetPoint; // shoot point
	[SerializeField] GameObject bullet;
	[SerializeField] float power;
	private Vector3 shootDirection;
    //[SerializeField] GameObject rightArm;
	//private GameObject armBone;
    //private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
	    
	    //armBone = GameObject.Find("bone_5");
        //transform.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
	    Debug.DrawRay(firePoint.transform.position, targetPoint.transform.position - firePoint.transform.position, Color.red);
	    //shootDirection = targetPoint.transform.position - firePoint.transform.position;
    }

    public void Shoot()
    {
	    //bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * power, ForceMode2D.Impulse);
	    Instantiate(bullet, firePoint.position, transform.rotation);
	    
       // rb.AddForce(new Vector2(transform.position.x * -0.2f, 0), ForceMode2D.Impulse);
        //print("PRot" + player.transform.rotation + "FP rot" + firePoint.rotation);
    }
}
