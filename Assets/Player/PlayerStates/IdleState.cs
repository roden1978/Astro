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
        

        if (_player.Direction.y == 1.0f)
        {
            _player.animator.SetTrigger("jump");
            return typeof(JumpState); 
        }


        if (_player.Direction.y == -1.0f || _player.UICrouchButton)
        {
            _player.animator.SetBool("crouch", true);
            return typeof(CrouchState);
        }
        
        if (_player.Direction.x == 0)
        {
            _player.animator.SetBool("walk", false);
            _player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);  
        }

        if (_player.IsChangeWeapon) return typeof(ChangeWeaponState);
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(IdleState);
    }
}
