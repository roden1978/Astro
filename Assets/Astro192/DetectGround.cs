﻿using UnityEngine;

public class DetectGround : MonoBehaviour
{
	
	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            GetComponentInParent<PlayerController>().Ground = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            GetComponentInParent<PlayerController>().Ground = false;
        }
        
        
    }
}
