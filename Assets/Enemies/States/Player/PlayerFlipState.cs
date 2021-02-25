using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipState : BaseState
{
    private Player _player;
    private System.Type _prevState;
    
    public PlayerFlipState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        if (_prevState == null) _prevState = _player.GetComponent<StateMashine>().GetPrevState;
        _player.MovingRight = !_player.MovingRight;
        transform.Rotate(0f, 180f, 0f);
        
        return _prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(PlayerFlipState);
    }
}
