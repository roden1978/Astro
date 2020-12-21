using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
    private GameObject player;
    
    private PlayerController playerController;
    private float shootDelay;
    
    public void OnPointerDown(PointerEventData eventData)
    {
	    if (playerController)
	    {
		    shootDelay = playerController.getWC.Weapon.ShootDelay <= 0 ? 0 : playerController.getWC.Weapon.ShootDelay; 
		    if (shootDelay > 0)
				InvokeRepeating("Shoot", shootDelay, shootDelay);
		    else Shoot();
	    }
	    
    }

    private void Shoot()
    {
	    playerController.Shoot();
    }
    

    public void OnPointerUp(PointerEventData eventData)
    {
	    if (shootDelay > 0) CancelInvoke("Shoot");
	    else playerController.StopShoot();
    }

    private void Start()
    {
	    playerController = player.GetComponent<PlayerController>();
    }

}
