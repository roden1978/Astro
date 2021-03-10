using UnityEngine;

public class JumpState : BaseState
{
    private readonly Player player;
    private System.Type prevState;
    
    public JumpState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;
        
        if (player.GroundCollider2D.GroundCheck("Ground"))
        {
            var force = Vector2.up * player.JumpForce;
            rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            player.UIJumpButton = false;
            return prevState;
        }
        if (player.IsChangeWeapon && !player.IsPlayerShooting) return typeof(ChangeWeaponState);
        
        if (player.UIStartShoot) return typeof(StartShootState);
        if (player.UIStopShoot) return typeof(StopShootState);
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(JumpState);
    }
}
