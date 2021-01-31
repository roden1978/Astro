using UnityEngine;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;
    protected Rigidbody2D rigidbody2D;
    
    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        rigidbody2D = gameObject.transform.GetComponent<Rigidbody2D>();
    }

    public abstract System.Type Tick();
    public abstract System.Type GetCurrentStateType();
}
