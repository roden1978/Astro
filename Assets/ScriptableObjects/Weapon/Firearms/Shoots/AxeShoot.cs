using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Axe Shoot", menuName = "Weapons/Firearms/Shoot/Axe Shoot")]
public class AxeShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		Debug.Log("Axe shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
