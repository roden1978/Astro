using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
    private GameObject player;
    
    private PlayerController playerController;
    
    public void OnPointerDown(PointerEventData eventData)
    {
	    if (playerController)
	    {
		    float shootDelay = playerController.getWC.Weapon.ShootDelay;
		    InvokeRepeating("Shoot", shootDelay, shootDelay);
	    }
	    
    }

    private void Shoot()
    {
	    playerController.Shoot();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
	    CancelInvoke("Shoot");
    }

    private void Start()
    {
	    playerController = player.GetComponent<PlayerController>();
    }

}
