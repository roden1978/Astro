using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loudcricket Shoot", menuName = "Weapons/Firearms/Shoot/Loudcricket Shoot")]
public class LoudcricketShoot : AWeaponShoot
{
#pragma warning disable 0649
    [SerializeField] [Tooltip("Пуля")] private GameObject bullet;

    [SerializeField] [Tooltip("Оружие")] private Weapon weapon;
#pragma warning restore 0649
    public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
    {
        if (weapon && bullet)
        {
            shootPoint = new Vector3(shootPoint.x, shootPoint.y + Random.Range(-0.1f, 0.1f), shootPoint.z);
            Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
            return Instantiate(bullet, shootPoint, rotation);
        }

        Debug.Log("Оружие или пуля не найдены скрипт LoudcricketShoot");

        return null;
    }
    
    public override void StopShoot()
    {
        throw new System.NotImplementedException();
    }
}
