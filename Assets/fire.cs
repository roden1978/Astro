using UnityEngine.EventSystems;
using UnityEngine;

public class fire : MonoBehaviour, IPointerDownHandler
{
    private astroGun astrogun;

    public void OnPointerDown(PointerEventData eventData)
    {
        astrogun = GameObject.FindGameObjectWithTag("weapon").GetComponent<astroGun>();

        if (astrogun != null)
        {
            if (gameObject.name == "FireButton")
            {
                astrogun.Shoot();
                //print("Fire");
            }
        } else
        {
            print("gun not found");
        }
        
    }

   
}
