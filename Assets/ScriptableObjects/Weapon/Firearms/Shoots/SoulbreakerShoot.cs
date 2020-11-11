using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Soulbreaker Shoot", menuName = "Weapons/Firearms/Shoot/Soulbreaker Shoot")]
public class SoulbreakerShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		Debug.Log("Soulbreaker shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
