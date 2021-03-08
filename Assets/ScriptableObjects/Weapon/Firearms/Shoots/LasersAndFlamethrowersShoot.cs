using UnityEngine;


[CreateAssetMenu(fileName = "New LasersAndFlamethrowersShoot", menuName = "Weapons/Shoots/LasersAndFlamethrowersShoot")]
public class LasersAndFlamethrowersShoot : AWeaponShoot
{
    private GameObject bulletGameObject;
    private GameObject muzzleGameObject;
    
    public override void Shoot(GameObject muzzleVFXPrefab, GameObject bullet, Vector3 shootPoint, Quaternion rotation)
    {
        if (!bulletGameObject)
        {
            bulletGameObject =  Instantiate(bullet, shootPoint, rotation);
            muzzleGameObject = Instantiate(muzzleVFXPrefab, shootPoint, rotation);
        } 
            
    }

    public override void StopShoot()
    {
        Destroy(bulletGameObject);
        Destroy(muzzleGameObject);
    }

    
}
