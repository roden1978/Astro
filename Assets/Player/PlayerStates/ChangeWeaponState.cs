using UnityEngine;

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

        player.ChangeWeapon();
        //Debug.Log($"Current weapon {currentWeapon}");
        /*if (currentWeapon)
        {
            player.CurrentWeapon = currentWeapon;
            player.Weapon = player.CurrentWeapon.GetComponent<Weapon>();
            player.IsChangeWeapon = false;
            player.SetupNewWeapon();

            return prevState;
        }
        
        return null;*/
        player.IsChangeWeapon = false;
        return prevState;
        
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(ChangeWeaponState);
    }
}