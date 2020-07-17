using UnityEngine.EventSystems;
using UnityEngine;

public class Crouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    private PlayerController playerController;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "CrouchButton")
        {
            playerController.SetCrouchButtonDown(!playerController.GetCrouchButtonDown());
            playerController.Crouch(); //SetDirection(new Vector2(playerController.GetDirection().x, 1));

            //print("jump");
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
       /* if (gameObject.name == "JumpButton")
        {
            playerController.SetDirection(new Vector2(playerController.GetDirection().x, 0));
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Astroman").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
