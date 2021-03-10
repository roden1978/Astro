using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jump : MonoBehaviour, IPointerDownHandler
{
   private Player player;

   public void OnPointerDown(PointerEventData eventData)
   {
       player.UIJumpButton = true;
   }

   private void Update()
   {
       if(!player) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
   }
}
