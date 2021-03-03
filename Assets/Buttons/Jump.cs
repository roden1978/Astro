using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerDownHandler
{
   private Player player;

   public void OnPointerDown(PointerEventData eventData)
   {
       if(!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       player.UIJumpButton = true;
   }

}
