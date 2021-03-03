using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Run : MonoBehaviour, IPointerDownHandler
{
	private Player player;
	private Image image;
	
	public void OnPointerDown(PointerEventData eventData)
	{
		try
		{
			if(!player){player = GameObject.FindWithTag("Player").GetComponent<Player>();}
			player.UIRunButton = player && !player.UIRunButton;
			image.color = player.UIRunButton ? Color.green : Color.white;
		}
		catch (Exception e)
		{
			Debug.Log(e);
			throw;
		}
		
	}

	// Start is called before the first frame update
	void Start()
	{
		try
		{
			image = gameObject.GetComponent<Image>();
		}
		catch (Exception e)
		{
			Debug.Log(e);
			throw;
		}
	}
}
