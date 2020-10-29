using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loudcricket Shoot", menuName = "Weapons/Firearms/Shoot/Loudcricket Shoot")]
public class LoudcricketShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint)
	{
		Debug.Log("Loudcricket shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
