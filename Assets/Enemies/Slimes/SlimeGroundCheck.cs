using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            GetComponentInParent<SlimeController>().Ground = true;
            print("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            GetComponentInParent<SlimeController>().Ground = false;
            print("exit");
        }


    }
}
