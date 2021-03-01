﻿using UnityEngine;

public class RunState : BaseState
{
    private Player _player;
    
    public RunState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
       
        if (_player.Direction.x == 0)
        {
            _player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            return typeof(IdleState);  
        }
        
        if (!_player.Run && !_player.UIRunButton)
        {
            _player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
           return typeof(MoveState);
        } 
      
        if (_player.Direction.y == 1.0f)
        {
            _player.animator.SetTrigger("jump");
            return typeof(JumpState);
        }
        
        if (_player.Direction.y == -1.0f  || _player.UICrouchButton)
        {
            _player.animator.SetBool("crouch", true);
            _player.animator.SetBool("walk", false);
            _player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            return typeof(CrouchState);
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

    public override void FixedTick()
    {
        Vector2 force = new Vector2(_player.Force.x * _player.Direction.x, 0.0f);
        if (Mathf.Abs(rigidbody2D.velocity.x) < _player.MaxRunVelocity)
        {
            rigidbody2D.AddForce(force, ForceMode2D.Force);
        }
                    
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(RunState);
    }
}
