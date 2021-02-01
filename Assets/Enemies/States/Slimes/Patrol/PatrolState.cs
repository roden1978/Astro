using UnityEngine;

public class PatrolState : BaseState
{
    private Slime _slime;
    
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
        if (Mathf.Abs(rigidbody2D.velocity.x) < _slime.SlimeData.maxVelocity)
        {
            if (_slime.MovingRight)
            {
                rigidbody2D.AddForce(Vector2.right * _slime.SlimeData.force, ForceMode2D.Force);
            }
            else
            {
                rigidbody2D.AddForce(Vector2.left * _slime.SlimeData.force, ForceMode2D.Force);
            }
        }
        
        if(transform.position.x > _slime.startPosition.x + _slime.SlimeData.patrolDistance && _slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (transform.position.x < _slime.startPosition.x - _slime.SlimeData.patrolDistance && !_slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (Vector2.Distance(transform.position, _slime.player.transform.position) < _slime.SlimeData.patrolDistance)
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
