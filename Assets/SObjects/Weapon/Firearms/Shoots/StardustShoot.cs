using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stardust Shoot", menuName = "Weapons/Firearms/Shoot/Stardust Shoot")]
public class StardustShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint)
	{
		Debug.Log("Stardust shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
