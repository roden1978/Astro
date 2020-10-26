using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeaponShoot : ScriptableObject
{
	public abstract void Shoot(Vector3 shootPoint);
}
