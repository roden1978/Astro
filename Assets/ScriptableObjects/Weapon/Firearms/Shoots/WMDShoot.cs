using UnityEngine;


[CreateAssetMenu(fileName = "New WMDShoot", menuName = "Weapons/Shoots/WMDShoot")]
public class WMDShoot : AWeaponShoot
{
    private GameObject bulletGameObject;
    private GameObject muzzleGameObject;
    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
        if (!bulletGameObject)
        {
            bulletGameObject = Instantiate(bullet, shootPoint, rotation);
            muzzleGameObject = Instantiate(muzzleVFXPrefab, shootPoint, rotation);
        }
    }

    public override void StopShoot()
    {
        Destroy(bulletGameObject);
        Destroy(muzzleGameObject);
    }
}