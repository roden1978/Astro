using UnityEngine;

public class IdleState : BaseState
{
    private readonly Player player;

    public IdleState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (player.Direction.x != 0)
        {
            player.animator.SetBool("walk", true);
            return typeof(MoveState);  
        }
        

        if (player.Direction.y == 1.0f)
        {
            player.animator.SetTrigger("jump");
            return typeof(JumpState); 
        }


        if (player.Direction.y == -1.0f || player.UICrouchButton)
        {
            player.animator.SetBool("crouch", true);
            return typeof(CrouchState);
        }
        
        if (player.Direction.x == 0)
        {
            player.animator.SetBool("walk", false);
            player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);  
        }

        if (player.IsChangeWeapon && !player.IsPlayerShooting) return typeof(ChangeWeaponState);
      
        if (player.UIStartShoot) return typeof(StartShootState);
        if (player.UIStopShoot) return typeof(StopShootState);
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(IdleState);
    }
}
