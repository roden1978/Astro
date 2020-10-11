using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroGun : MonoBehaviour
{
    #pragma warning disable 0649
	[SerializeField] Transform shootPoint; // shoot point
    [SerializeField] Transform targetPoint; // shoot point
	[SerializeField] GameObject bullet;
	#pragma warning restore 0649
    // Start is called before the first frame update
    void Start()
    {
	    
	    //for (int i = 0; i < transform.childCount -1; i++){
	    //	if(transform.GetChild(i).name == "shootPoint"){
	    //		shootPoint = transform.GetChild(i).transform;
	    //	}
	    //}
	    
	    //if(shootPoint != null){
	    //	Debug.Log("shootPoint " + shootPoint);
	    //}
	    //armBone = GameObject.Find("bone_5");
        //transform.Rotate(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
	    if(shootPoint != null)
	    	Debug.DrawRay(shootPoint.position, targetPoint.position - shootPoint.position, Color.red);
	    //shootDirection = targetPoint.transform.position - firePoint.transform.position;
    }

    public void Shoot()
    {
	    //bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * power, ForceMode2D.Impulse);
	    Instantiate(bullet, shootPoint.position, transform.rotation);
	    
       // rb.AddForce(new Vector2(transform.position.x * -0.2f, 0), ForceMode2D.Impulse);
        //print("PRot" + player.transform.rotation + "FP rot" + firePoint.rotation);
    }
    
	public Transform ShootPoint {
		get {
			return shootPoint;
		}
	}
	
	public Transform TargetPoint {
		get 
		{
			return targetPoint;
		}
	}
}
