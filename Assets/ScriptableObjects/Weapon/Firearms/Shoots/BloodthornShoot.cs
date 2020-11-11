using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bloodthorn Shoot", menuName = "Weapons/Firearms/Shoot/Bloodthorn Shoot")]
public class BloodthornShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		Debug.Log("Bloodthorn shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
