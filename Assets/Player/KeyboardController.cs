using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : IController
{
    public float GetVerticalDirection()
    {
        return Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0;
    }

    public float GetHorizontalDirection()
    {
        
        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetRun()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
