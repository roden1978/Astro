public class CrouchState : BaseState
{
    private readonly Player player;
    private System.Type prevState;
    
    public CrouchState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;
        //UI
        if (!player.UICrouchButton)
        {
            player.animator.SetBool("crouch", false);
            return prevState;
        }
       
        //keyboard
        /*if (!player.UICrouchButton && (int) player.Direction.y == 1 || (int) player.Direction.y == -1)
        {
            player.animator.SetBool("crouch", false);
            return prevState;
        }*/
        
        if (player.IsChangeWeapon && !player.IsPlayerShooting) return typeof(ChangeWeaponState);
        
        if (player.UIStartShoot) return typeof(StartShootState);
        if (player.UIStopShoot) return typeof(StopShootState);
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(CrouchState);
    }
}
