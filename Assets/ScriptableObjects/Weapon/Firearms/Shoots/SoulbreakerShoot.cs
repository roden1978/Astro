using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Soulbreaker Shoot", menuName = "Weapons/Firearms/Shoot/Soulbreaker Shoot")]
public class SoulbreakerShoot : AWeaponShoot
{
#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
#pragma warning restore 0649

	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		if (weapon && bullet) {
			Instantiate(weapon.VFXShoot, shootPoint, weapon.ShootPointRotation);
			for (int i = 0; i <= 2; i++)
			{
				Instantiate(bullet, shootPoint, rotation );
			}
		}

		Debug.Log("Оружие или пуля не найдены скрипт SoulbreakerShoot");

		return null;
	}
	
	public override void StopShoot()
	{
		throw new System.NotImplementedException();
	}
	
}
