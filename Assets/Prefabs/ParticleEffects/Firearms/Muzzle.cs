using UnityEngine;

public class Muzzle : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private WeaponSettings weaponSettings;
#pragma warning restore 0649
	
   private void FixedUpdate()
    {
        transform.position = weaponSettings.ShootPoint;
        transform.rotation = weaponSettings.ShootPointRotation;
    }
   
  
}
