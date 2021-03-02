using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerDownHandler
{
   
	public void OnPointerDown(PointerEventData eventData)
	{
		Application.Quit();
		
		if (Application.platform == RuntimePlatform.Android) {
			AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
			activity.Call("finishAndRemoveTask");
		}
	}
}
