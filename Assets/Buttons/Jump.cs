using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private GameObject player;
    private Player playerController;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.name == "JumpButton")
        {
            //playerController.Jump(); //SetDirection(new Vector2(playerController.GetDirection().x, 1));

            //print("jump" + playerController.GetDirection());
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameObject.name == "JumpButton")
        {
            playerController.Direction = new Vector2(playerController.Direction.x, 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
	    playerController = player.GetComponent<Player>();
	    
	    if (!playerController){
	    	print ("Jump script Player not found");
	    } else {
	    	print ("Jump script Player found");
	    }
    }

}
