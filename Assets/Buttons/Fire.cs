using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
    private GameObject player;

    private PlayerController playerController;
    public void OnPointerDown(PointerEventData eventData)
    {
	    if (!playerController) playerController = player.GetComponent<PlayerController>();
	    else InvokeRepeating("Shoot", 0.1f, 0.1f);
	    
    }

    private void Shoot()
    {
	    playerController.Shoot();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
	    CancelInvoke("Shoot");
    }
}
