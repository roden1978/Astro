using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerDownHandler
{
   
	public void OnPointerDown(PointerEventData eventData)
	{
		if (gameObject.name == "QuitButton")
		{
			ApplicationQuit();
		}
	}
    
	public void ApplicationQuit () {
		Application.Quit();
		
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("finishAndRemoveTask");
		}
		
		Debug.Log("Quit");
	}
}
