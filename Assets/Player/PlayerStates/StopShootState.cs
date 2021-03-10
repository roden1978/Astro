using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopShootState : BaseState
{
    private readonly Player player;
    private System.Type prevState;
    
    public StopShootState(Player player) : base(player.gameObject)
    {
        this.player = player;
    }

    public override System.Type Tick()
    {
        if (prevState == null) prevState = player.GetComponent<StateMashine>().GetPrevState;
        if (player.UIStopShoot)
        {
            player.StoppingShoot();
            player.UIStopShoot = false;
            player.IsPlayerShooting = false;
        }
        
        return prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(StopShootState);
    }
}
