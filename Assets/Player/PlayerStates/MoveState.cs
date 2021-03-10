using UnityEngine;

public class MoveState : BaseState
{
    private Player player;
    
    public MoveState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (player.Run || player.UIRunButton)
        {
            player.animator.SetBool("run", true);
            return typeof(RunState);
        }
        
        if (player.Direction.x == 0)
        {
            player.animator.SetBool("walk", false);
            player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            return typeof(IdleState);  
        }

        if (player.Direction.y == 1.0f)
        {
            player.animator.SetTrigger("jump");
            return typeof(JumpState);
        }

        if (player.Direction.y == -1.0f  || player.UICrouchButton)
        {
            player.animator.SetBool("crouch", true);
            player.animator.SetBool("walk", false);
            player.animator.SetBool("run", false);
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            return typeof(CrouchState);
        }

        if (player.Direction.x < 0 && player.MovingRight)
        {
            return typeof(PlayerFlipState);
        }

        if (player.Direction.x > 0 && !player.MovingRight)
        {
            return typeof(PlayerFlipState);
        }
        
        if (player.IsChangeWeapon && !player.IsPlayerShooting) return typeof(ChangeWeaponState);
        
        if (player.UIStartShoot) return typeof(StartShootState);
        if (player.UIStopShoot) return typeof(StopShootState);

        return null;
    }

    public override void FixedTick()
    {
        Vector2 force = new Vector2(player.Force.x * player.Direction.x, 0.0f);

        if (Mathf.Abs(rigidbody2D.velocity.x) < player.MaxWalkVelocity)
        {
            rigidbody2D.AddForce(force, ForceMode2D.Force);
        }
        
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(MoveState);
    }
}
