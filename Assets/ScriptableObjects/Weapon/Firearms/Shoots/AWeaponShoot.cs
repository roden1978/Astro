using UnityEngine;

public abstract class AWeaponShoot : ScriptableObject
{
	public abstract GameObject Shoot(Vector3 shootPoint, Quaternion rotation);
	public abstract void StopShoot();
}
