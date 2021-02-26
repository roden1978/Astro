using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseState
{
    private Player _player;
    private System.Type _prevState;
    
    public CrouchState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        
        if (_prevState == null) _prevState = _player.GetComponent<StateMashine>().GetPrevState;
        
        if ((int) _player.Direction.y == 1)
        {
            _player.animator.SetBool("crouch", false);
            return _prevState;
        }
        
        if (_player.Direction.x < 0 && _player.MovingRight)
        {
            return typeof(PlayerFlipState);
        }

        if (_player.Direction.x > 0 && !_player.MovingRight)
        {
            return typeof(PlayerFlipState);
        }
       
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(CrouchState);
    }
}
