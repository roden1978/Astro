using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private Slime _slime;
    private float chaseDistance = 4;
    public ChaseState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
    }

    public override System.Type Tick()
    {
        if (Vector2.Distance(_slime.transform.position, _slime.player.transform.position) > chaseDistance)
        {
            Debug.Log("Patrol");
            return typeof(PatrolState);
        }
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(ChaseState);
    }
}
