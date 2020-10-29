using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agressor Shoot", menuName = "Weapons/Firearms/Shoot/Agressor Shoot")]
public class AgressorShoot : AWeaponShoot
{
	public override GameObject Shoot(Vector3 shootPoint)
	{
		Debug.Log("Agressor shoot");
		return null;
		//throw new System.NotImplementedException();
	}
}
