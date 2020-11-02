using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stardust Shoot", menuName = "Weapons/Firearms/Shoot/Stardust Shoot")]
public class StardustShoot : AWeaponShoot
{
#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
#pragma warning restore 0649
	public override GameObject Shoot(Vector3 shootPoint)
	{
		if (weapon && bullet) {
			return Instantiate(bullet, shootPoint, Quaternion.identity) as GameObject;
		} else {
			Debug.Log("Оружие или пуля не найдены скрипт StardustShoot");
		}
		
		return null;
	}
}
