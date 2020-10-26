using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Shoot", menuName = "Weapons/Firearms/Shoot/Gun Shoot")]
public class GunShoot : AWeaponShoot
{
	[SerializeField]
	private GameObject bullet;
	
	[SerializeField]
	private Weapon weapon;
	

	public override void Shoot(Vector3 shootPoint)
	{
		GameObject bul;
		bul = Instantiate(bullet, shootPoint, Quaternion.identity) as GameObject;
		//
		//if(weapon){
		//	bul = Instantiate(bullet, weapon.WeaponShootPointPosition, Quaternion.identity) as GameObject;
		//	Debug.Log("Gun Shoot " + bul);
		//}else{
		//	Debug.Log("Weapon Not Found");
		//}
		
		
		//if(weapon.WeaponItem){
		//	bul = Instantiate(bullet, weapon.WeaponItem.transform.Find("shootPoint").position, weapon.WeaponItem.transform.rotation) as GameObject;
		//	Debug.Log("Gun Shoot " + bul);
		//}else{
		//	Debug.Log("Weapon Not Found");
		//}
		//throw new System.NotImplementedException();
	}
}
