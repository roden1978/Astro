using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private Player player;
    private float shootDelay;
    
    public void OnPointerDown(PointerEventData eventData)
    {
	    if (player)
	    {
		    shootDelay = player.GetWeaponController.Weapon.ShootDelay <= 0 ? 0 : player.GetWeaponController.Weapon.ShootDelay; 
		    if (shootDelay > 0)
				InvokeRepeating("Shoot", 0, shootDelay);
		    else Shoot();
	    }
	    
    }

    private void Shoot()
    {
	    player.Shoot();
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
	    if (shootDelay > 0)
	    {
		    player.GetWeaponController.Weapon.IsShooting = false;
		    CancelInvoke("Shoot");
	    }
	    else player.StopShoot();
    }

    private void Start()
    {
	    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

}
