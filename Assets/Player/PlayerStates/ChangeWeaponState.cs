public class ChangeWeaponState : BaseState
{
    private Player player;
    private System.Type prevState;

    public ChangeWeaponState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;

        player.CurrentWeapon = player.ChangeWeapon();
        
        if (player.CurrentWeapon)
        {
            player.Weapon = player.CurrentWeapon.GetComponent<Weapon>();
            player.SetupNewWeapon();
            player.IsChangeWeapon = false;

            return prevState;
        }
        
        return null;
        
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(ChangeWeaponState);
    }
}