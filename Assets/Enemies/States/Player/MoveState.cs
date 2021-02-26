using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    private Player _player;
    
    public MoveState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _player.animator.SetBool("walk", false);
            _player.animator.SetBool("run", true);
            //_player.animator.SetBool("crouch", false);
            return typeof(RunState);
        }
        
        
        if (_player.Direction.x == 0)
        {
            _player.animator.SetBool("walk", false);
            _player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            return typeof(IdleState);  
        }

        if ((int)_player.Direction.y == 1)
        {
            _player.animator.SetTrigger("jump");
            return typeof(JumpState);
        }

        if ((int)_player.Direction.y == -1)
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

        /*if (_player.Direction.x != 0)
        {*/
            
        //}
        return null;
    }

    public override void FixedTick()
    {
        Vector2 force = new Vector2(_player.Force.x * _player.Direction.x, 0.0f);
                
                    //Debug.Log($"Direction X {force}");
                    if (Mathf.Abs(rigidbody2D.velocity.x) < _player.MaxWalkVelocity)
                    {
                        rigidbody2D.AddForce(force, ForceMode2D.Force);
                    }
        
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(MoveState);
    }
}
