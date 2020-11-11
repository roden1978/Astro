using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Widowmaker Shoot", menuName = "Weapons/Firearms/Shoot/Widowmaker Shoot")]
public class WidowmakerShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		Debug.Log("Widowmaker shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
