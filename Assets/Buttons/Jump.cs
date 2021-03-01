using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerUpHandler
{
   private Player player;
   
    public void OnPointerUp(PointerEventData eventData)
    {
        player.Jump();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

}
