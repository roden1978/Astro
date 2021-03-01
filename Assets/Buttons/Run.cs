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
		player.UIRunButton = player && !player.UIRunButton;
		image.color = player.UIRunButton ? Color.green : Color.white;
	}

	// Start is called before the first frame update
	void Start()
	{
		try
		{
			player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
	}
}
