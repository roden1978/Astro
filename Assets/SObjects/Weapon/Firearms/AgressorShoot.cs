using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Agressor Shoot", menuName = "Weapons/Firearms/Shoot/Agressor Shoot")]
public class AgressorShoot : AWeaponShoot
{
	public override void Shoot(Vector3 shootPoint)
	{
		Debug.Log("Agressor shoot");
		//throw new System.NotImplementedException();
	}
}
