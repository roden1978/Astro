using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lightstone Shoot", menuName = "Weapons/Firearms/Shoot/Lightstone Shoot")]
public class LightstoneShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint, Quaternion rotation)
	{
		Debug.Log("Lightstone shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
