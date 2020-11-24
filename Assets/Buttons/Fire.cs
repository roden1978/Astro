using UnityEngine.EventSystems;
using UnityEngine;

public class Fire : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
    private GameObject player;

    private PController pController;
    public void OnPointerDown(PointerEventData eventData)
    {
	    if (!pController) pController = player.GetComponent<PController>();
	    else InvokeRepeating("Shoot", 0.1f, 0.1f);
	    
    }

    private void Shoot()
    {
	    pController.Shoot();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
	    CancelInvoke("Shoot");
    }
}
