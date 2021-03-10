using UnityEngine;


[CreateAssetMenu(fileName = "New GunsShoot", menuName = "Weapons/Shoots/GunsShoot")]
public class GunsShoot : AWeaponShoot
{

    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
            Instantiate(bullet, shootPoint, rotation);
            Instantiate(muzzleVFXPrefab, shootPoint, rotation);
    }

    public override void StopShoot()
    {
        
    }

    
}
