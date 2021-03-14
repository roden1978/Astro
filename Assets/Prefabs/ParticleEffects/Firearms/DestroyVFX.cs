using System.Collections;
using UnityEngine;

public class DestroyVFX : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Задержка уничтожения объекта")]
    private float delay;
   
    private IEnumerator coroutine;
    
    // Start is called before the first frame update
    /*void Start()
    {
        coroutine = Die(delay);
        StartCoroutine(coroutine);
    }*/
    public void OnEnable()
    {
        Invoke("Destroy", delay);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    /*private IEnumerator Die(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }*/

    public float Delay
    {
        get => delay;
    }
}
