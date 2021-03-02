using UnityEngine;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;
    protected Rigidbody2D rigidbody2D;

    protected BaseState(GameObject _gameObject)
    {
        gameObject = _gameObject;
        transform = gameObject.transform;
        rigidbody2D = gameObject.transform.GetComponent<Rigidbody2D>();
    }

    public abstract System.Type Tick();
    public virtual void FixedTick(){}
    public abstract System.Type GetCurrentStateType();
}
