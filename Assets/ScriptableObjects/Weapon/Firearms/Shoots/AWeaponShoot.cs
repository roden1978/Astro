using UnityEngine;

public abstract class AWeaponShoot : ScriptableObject
{
	public abstract void Shoot(Vector3 shootPoint, Quaternion rotation);
	public abstract void StopShoot();
}
