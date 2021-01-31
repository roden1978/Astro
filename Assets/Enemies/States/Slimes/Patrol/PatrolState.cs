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
        if (_slime.MovingRight)
        {
            rigidbody2D.AddForce(Vector2.right * _slime.SlimeData.force, ForceMode2D.Force);
        }
        else 
        {
            rigidbody2D.AddForce(Vector2.left * _slime.SlimeData.force, ForceMode2D.Force);
        }
        
        if(transform.position.x > _slime.startPosition.x + patrolDistance && _slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (transform.position.x < _slime.startPosition.x - patrolDistance && !_slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (Vector2.Distance(_slime.transform.position, _slime.player.transform.position) < patrolDistance)
        {
            return typeof(ChaseState);
        }
        
        return null;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(PatrolState);
    }

}
