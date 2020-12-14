using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bloodthorn Shoot", menuName = "Weapons/Firearms/Shoot/Bloodthorn Shoot")]
public class BloodthornShoot : AWeaponShoot
{
#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
#pragma warning restore 0649
	private GameObject bulletGameObject;
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		if (!bulletGameObject)
		{
			CreateFlame(shootPoint, rotation);
		}else Destroy(bulletGameObject);

		return bulletGameObject;
	}

	private GameObject CreateFlame(Vector3 _shootPoint, Quaternion _rotation)
	{
		if (weapon && bullet) {
        			//Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
                    bulletGameObject =  Instantiate(bullet, _shootPoint, _rotation);
        }
        
        Debug.Log("Оружие или пуля не найдены скрипт BloodthornShoot");
        
        return null;
	}
}
