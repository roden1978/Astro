using UnityEngine;

public class DetectGround : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ground")
        {
            GetComponentInParent<PlayerController>().SetGround(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ground")
        {
            GetComponentInParent<PlayerController>().SetGround(false);
        }
        
        
    }
}
