using UnityEngine.EventSystems;
using UnityEngine;

public class fire : MonoBehaviour, IPointerDownHandler
{
	private WeaponController wc;

    public void OnPointerDown(PointerEventData eventData)
    {
	    wc = GameObject.FindGameObjectWithTag("weapon").GetComponent<WeaponController>();

	    if (wc != null)
        {
            if (gameObject.name == "FireButton")
            {
	            wc.Shoot();
                //print("Fire");
            }
        } else
        {
            print("gun not found");
        }
        
    }

   
}
