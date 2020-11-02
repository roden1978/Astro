using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Alligator Shoot", menuName = "Weapons/Firearms/Shoot/Alligator Shoot")]
public class AlligatorShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint)
	{
		Debug.Log("Alligator shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
