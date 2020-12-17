using UnityEngine;

public class MuzzleController : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Weapon gun;
#pragma warning restore 0649
	
   private void FixedUpdate()
    {
        transform.position = gun.ShootPoint;
        transform.rotation = gun.ShootPointRotation;
    }
}
