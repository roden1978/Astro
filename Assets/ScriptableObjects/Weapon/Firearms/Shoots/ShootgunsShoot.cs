using UnityEngine;


[CreateAssetMenu(fileName = "New ShootgunsShoot", menuName = "Weapons/Shoots/ShootgunsShoot")]
public class ShootgunsShoot : AWeaponShoot
{

    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
        Instantiate(muzzleVFXPrefab, shootPoint, rotation);
        for (int i = 0; i <= 2; i++)
        {
            Instantiate(bullet, shootPoint, rotation);
        }
    }

    public override void StopShoot()
    {
        
    }

    
}
