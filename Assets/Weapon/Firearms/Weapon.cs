using UnityEngine;

public class Weapon: MonoBehaviour
{
    #pragma warning disable 0649
	[SerializeField] [Tooltip("Точка выстрела")] Transform shootPoint; // shoot point
	
	[SerializeField] [Tooltip("Точка направления стрельбы")]Transform targetPoint; // target point

	[SerializeField] WeaponSettings weaponSettings;
	#pragma warning restore 0649
    // Start is called before the first frame update
    void Start()
    {
	    weaponSettings.ShootPoint = shootPoint.position;
	    weaponSettings.TargetPoint = targetPoint.position;
	    weaponSettings.ShootPointRotation = shootPoint.rotation;
    }

    // Update is called once per frame
    void Update()
    {
	    weaponSettings.ShootPoint = shootPoint.position;
	    weaponSettings.TargetPoint = targetPoint.position;
	    weaponSettings.ShootPointRotation = shootPoint.rotation;
	  
	    if(shootPoint)
	    {
		    var position = shootPoint.position;
		    Debug.DrawRay(position, targetPoint.position - position, Color.red);
	    }
	    
    }

    public void Shoot()
    {
	    weaponSettings.WeaponShoot.Shoot(weaponSettings.MuzzleVFX,weaponSettings.BulletPrefab, 
		    weaponSettings.ShootPoint, weaponSettings.ShootPointRotation);
    }
    public void StopShoot()
    {
	    weaponSettings.WeaponShoot.StopShoot();
    }
   
	public WeaponSettings WeaponSettings => weaponSettings;
}
