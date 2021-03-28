using UnityEngine;
using UnityEngine.Events;

public abstract class ABullet : MonoBehaviour
{
    protected int damage;
    protected UnityEvent showEffect;

    protected void Awake()
    {
        showEffect = new UnityEvent();
        showEffect.AddListener(Play);
    }

    protected virtual void Play(){}
}
