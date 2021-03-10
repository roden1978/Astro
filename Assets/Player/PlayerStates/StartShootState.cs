using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShootState : BaseState
{
    private readonly Player player;
    private System.Type prevState;
    
    public StartShootState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;
        
        if(player.UIStartShoot)
        {
            player.StartingShoot();
            player.UIStartShoot = false;
            player.IsPlayerShooting = true;
        }

        //if(!player.UIFireButton) return typeof(StopShootState);
        
        return prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(StartShootState);
    }
}
