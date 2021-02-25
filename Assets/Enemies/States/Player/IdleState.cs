using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private Player _player;

    public IdleState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        if (_player.Direction.x != 0)
        {
            _player.animator.SetBool("walk", true);
            return typeof(MoveState);  
        }

        if ((int) _player.Direction.y == 1)
        {
            _player.animator.SetTrigger("jump");
            //_player.isCanJump = false;
            return typeof(JumpState); 
        }


        if ((int)_player.Direction.y == -1)
        {
            _player.animator.SetBool("crouch", true);
            return typeof(CrouchState);
        }
        
        /*if (_player.GroundCollider2D.GroundCheck("Ground"))
        {
            _player.isCanJump = true;
        }*/
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(IdleState);
    }
}
