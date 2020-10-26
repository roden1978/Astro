using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroGun : MonoBehaviour
{
    #pragma warning disable 0649
	[SerializeField] Transform shootPoint; // shoot point
	
	[SerializeField] Transform targetPoint; // target point

	//[SerializeField] GameObject bullet;
	
	[SerializeField] Weapon weapon;
	#pragma warning restore 0649
	
	
    // Start is called before the first frame update
    void Start()
    {
	    weapon.ShootPoint = shootPoint.position;
	    weapon.TargetPoint = targetPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
	    weapon.ShootPoint = shootPoint.position;
	    weapon.TargetPoint = targetPoint.position;
	    
	    if(shootPoint != null)
	    Debug.DrawRay(shootPoint.position, targetPoint.position - shootPoint.position, Color.red);
	    //shootDirection = targetPoint.transform.position - firePoint.transform.position;
    }

    public void Shoot()
    {
	    weapon.Shoot();
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
	
	public Weapon Weapon {
		get {
			return weapon;
		}
	}
}
