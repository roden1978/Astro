using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private Slime _slime;

    private float patrolDistance = 3f;
    
    public PatrolState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
    }

    public override System.Type Tick()
    {
        if (_slime.player == null)
        {
            _slime.SetTarget(GameObject.FindGameObjectWithTag("Player"));
        }

        if (Vector2.Distance(_slime.transform.position, transform.position) < patrolDistance)
        {
            return typeof(ChaseState);
        }

        return null;
    }
}
