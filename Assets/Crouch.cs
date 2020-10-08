using UnityEngine.EventSystems;
using UnityEngine;

public class Crouch : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    private PController playerController;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playerController != null)
        {
            if (gameObject.name == "CrouchButton")
            {
                playerController.CrouchButtonDown = !playerController.CrouchButtonDown;
                playerController.Crouch();
            }
        } else
        {
            print("player not found");
        }
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
	    playerController = GameObject.FindWithTag("Player").GetComponent<PController>();
    }

 }
