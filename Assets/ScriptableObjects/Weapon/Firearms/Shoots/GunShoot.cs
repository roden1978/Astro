using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Shoot", menuName = "Weapons/Firearms/Shoot/Gun Shoot")]
public class GunShoot : AWeaponShoot
{
	#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
	#pragma warning restore 0649
	
	private IEnumerator coroutine;

	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		
		if (weapon && bullet)
		{
			Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
			return Instantiate(bullet, shootPoint, Quaternion.identity);
		} else {
			Debug.Log("Оружие или пуля не найдены скрипт GunShoot");
		}
		
		return null;
		
	}
	
	
}
