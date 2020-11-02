using UnityEngine;
using UnityEngine.EventSystems;

public class Run : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private PController playerController;
	public void OnPointerDown(PointerEventData eventData)
	{
		if(gameObject.name == "RunButton")
		{
			playerController.ChangeWeapon();
		}
		Debug.Log("name" + gameObject.name);

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("Run button up");
	}

	// Start is called before the first frame update
	void Start()
	{
		playerController = GameObject.FindWithTag("Player").GetComponent<PController>();
	    
		if (!playerController){
			print ("Run script Player not found");
		} else {
			print ("Run script Player found");
		}
	}

	// Update is called once per frame
	void Update()
	{
        
	}
}
