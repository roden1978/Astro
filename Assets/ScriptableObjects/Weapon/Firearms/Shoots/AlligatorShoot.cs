using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Alligator Shoot", menuName = "Weapons/Firearms/Shoot/Alligator Shoot")]
public class AlligatorShoot : AWeaponShoot
{
#pragma warning disable 0649
	[SerializeField]
	[Tooltip("Пуля")]
	private GameObject bullet;
	
	[SerializeField]
	[Tooltip("Оружие")]
	private Weapon weapon;
#pragma warning restore 0649
	private GameObject laserGameObject;
	private GameObject muzzleGameObject;
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		if (!laserGameObject) CreateLaser(shootPoint, rotation);
		return laserGameObject;
	}

	private void CreateLaser(Vector3 _shootPoint, Quaternion _rotation)
	{
		if (weapon && bullet) {
			muzzleGameObject = Instantiate(weapon.VFXShoot, _shootPoint, weapon.ShootPointRotation);
			laserGameObject =  Instantiate(bullet, _shootPoint, _rotation);
		}else Debug.Log("Оружие или пуля не найдены скрипт AlligatorShoot");
	}
	
	public override void StopShoot()
	{
		Destroy(laserGameObject);
		Destroy(muzzleGameObject);
	}
}
