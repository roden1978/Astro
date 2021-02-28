using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : IController
{
   
    private Joystick _viewJoystick;
    private float joystickDelay;
    private Player _player;
    
    public UIController()
        {
            joystickDelay = 0.3f;
            _player = GameObject.Find("Player").GetComponent<Player>();
            _viewJoystick = _player.playerSettings.joystick;
        }
    public float GetVerticalDirection()
    {
        return _viewJoystick.Vertical;
    }

    public float GetHorizontalDirection()
    {
        if (_viewJoystick.Horizontal > joystickDelay)
        {
            return _viewJoystick.Horizontal;
        }

        return 0;
    }

    public bool GetRun()
    {
        return false;
    }
}
