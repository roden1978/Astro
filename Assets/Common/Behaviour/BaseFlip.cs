using UnityEngine;

public class BaseFlip : IFlipable
{
    public void Flip(Transform transform)
    {
        transform.Rotate(0f, 180f, 0f);
    }
}
