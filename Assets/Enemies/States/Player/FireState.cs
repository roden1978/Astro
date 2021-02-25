using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState : BaseState
{
    private Player _player;
    
    public FireState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        throw new System.NotImplementedException();
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(FireState);
    }
}
