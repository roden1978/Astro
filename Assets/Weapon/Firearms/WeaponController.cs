using UnityEngine;

public class WeaponController: MonoBehaviour
{
    #pragma warning disable 0649
	[SerializeField] [Tooltip("Точка выстрела")] Transform shootPoint; // shoot point
	
	[SerializeField] [Tooltip("Точка направления стрельбы")]Transform targetPoint; // target point

	[SerializeField] Weapon weapon;
	#pragma warning restore 0649
	
	
    // Start is called before the first frame update
    void Start()
    {
	    weapon.ShootPoint = shootPoint.position;
	    weapon.TargetPoint = targetPoint.position;
	    weapon.ShootPointRotation = shootPoint.rotation;
    }

    // Update is called once per frame
    void Update()
    {
	    weapon.ShootPoint = shootPoint.position;
	    weapon.TargetPoint = targetPoint.position;
	    weapon.ShootPointRotation = shootPoint.rotation;
	    //Debug.Log($"ShootPoint rotation {shootPoint.rotation}");
	    
	    if(shootPoint)
	    {
		    var position = shootPoint.position;
		    Debug.DrawRay(position, targetPoint.position - position, Color.red);
	    }

	    //shootDirection = targetPoint.transform.position - firePoint.transform.position;
    }

    public void Shoot()
    {
	    weapon.Shoot();
    }
    
	/*public Transform ShootPoint {
		get {
			return shootPoint;
		}
	}
	
	public Transform TargetPoint {
		get 
		{
			return targetPoint;
		}
	}*/
	
	public Weapon Weapon => weapon;
}
