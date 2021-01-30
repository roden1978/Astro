using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private Slime _slime;

    private float patrolDistance = 3f;
    private float maxVelocity = 1;
    
    public PatrolState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
        if (_slime.player == null)
        {
            _slime.SetTarget(GameObject.FindGameObjectWithTag("Player"));
        }
    }

    public override System.Type Tick()
    {
        
        if (Vector2.Distance(_slime.transform.position, _slime.player.transform.position) < patrolDistance)
        {
            //Debug.Log("Flip state");
            return typeof(FlipState);
        }
        //Debug.Log("Patrol state");
        rigidbody2D.AddForce(Vector2.right * 0.5f, ForceMode2D.Force);
        
        rigidbody2D.VelocityControl(maxVelocity);
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(PatrolState);
    }

}
