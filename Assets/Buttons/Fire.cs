using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private Player player;
    //private float shootDelay;
    
    public void OnPointerDown(PointerEventData eventData)
    {
		    player.UIStartShoot = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
		    player.UIStopShoot = true;
    }

    private void Update()
    {
	    if(!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}
