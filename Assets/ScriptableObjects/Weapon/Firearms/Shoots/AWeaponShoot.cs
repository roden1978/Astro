using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponShoot : ScriptableObject
{
	public abstract GameObject Shoot(Vector3 shootPoint, Quaternion rotation);
}
