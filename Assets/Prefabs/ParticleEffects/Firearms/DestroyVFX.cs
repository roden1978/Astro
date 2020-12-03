using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DestroyVFX : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Задержка уничтожения объекта")]
    private float delay;
    
    [SerializeField]
    [Tooltip("Задержка выключения вспышки")]
    private float flashDelay;
   
    private IEnumerator coroutine;
    private IEnumerator lightCoroutine;

    private Light2D flash;
    
    // Start is called before the first frame update
    void Start()
    {
        flash = transform.Find("Flash").GetComponent<Light2D>();
        
        coroutine = Die(delay);
        lightCoroutine = LightOff(flashDelay);
        StartCoroutine(coroutine);
        if (flash) StartCoroutine(lightCoroutine);
    }

    private IEnumerator Die(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
    
    private IEnumerator LightOff(float _flashDelay)
    {
        yield return new WaitForSeconds(_flashDelay);
        flash.intensity = 0;
    }
}
