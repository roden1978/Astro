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
        float pDistance = _slime.SlimeData.PatrolDistance;
        
        if(transform.position.x > _slime.startPosition.x + pDistance && _slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (transform.position.x < _slime.startPosition.x - pDistance && !_slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (Vector2.Distance(transform.position, _slime.player.transform.position) < pDistance)
        {
            return typeof(ChaseState);
        }
        
        return null;
    }

    public override void FixedTick()
    {
        float force = _slime.SlimeData.Force;
        
        if (Mathf.Abs(rigidbody2D.velocity.x) < _slime.SlimeData.GetMaxVelocity)
        {
            if (_slime.MovingRight)
            {
                rigidbody2D.AddForce(Vector2.right * force, ForceMode2D.Force);
            }
            else
            {
                rigidbody2D.AddForce(Vector2.left * force, ForceMode2D.Force);
            }
        }
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(PatrolState);
    }

}
