using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Run : MonoBehaviour, IPointerDownHandler
{
	private Player playerController;
	private Image image;
	
	public void OnPointerDown(PointerEventData eventData)
	{
		playerController.UIRunButton = playerController && !playerController.UIRunButton;
		image.color = playerController.UIRunButton ? Color.green : Color.white;
	}

	// Start is called before the first frame update
	void Start()
	{
		try
		{
			playerController = GameObject.FindWithTag("Player").GetComponent<Player>();
		}
		catch (Exception e)
		{
			Debug.Log(e);
			throw;
		}

		try
		{
			image = gameObject.GetComponent<Image>();
		}
		catch (Exception e)
		{
			Debug.Log(e);
			throw;
		}
		
		
		//print(!playerController ? "Run script Player not found" : "Run script Player found");
		//print(!image ? "Run script Image not found" : "Run script Image found");
	}
}
