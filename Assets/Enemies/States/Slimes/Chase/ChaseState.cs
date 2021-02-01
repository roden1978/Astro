using UnityEngine;

public class ChaseState : BaseState
{
    private Slime _slime;
    public ChaseState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
    }

    public override System.Type Tick()
    {
        if (Vector2.Distance(transform.position, _slime.player.transform.position) > _slime.SlimeData.stopChaseDistance)
        {
            return typeof(PatrolState);
        }

        if (_slime.PlayerSideDetect(transform) != _slime.MovingRight)
        {
            return typeof(FlipState);
        }
        
        if (Mathf.Abs(rigidbody2D.velocity.x) < _slime.SlimeData.maxVelocity + _slime.SlimeData.maxVelocity / 2)
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
        /*if (_slime.MovingRight)
        {
            rigidbody2D.AddForce(Vector2.right * _slime.SlimeData.force, ForceMode2D.Force);
        }
        else 
        {
            rigidbody2D.AddForce(Vector2.left * _slime.SlimeData.force, ForceMode2D.Force);
        }*/
        
        if (Vector2.Distance(transform.position, _slime.player.transform.position) < _slime.SlimeData.attackDistance)
        {
            return typeof(AttackState);
        }
        
        
        return null;
    }
    

    public override System.Type GetCurrentStateType()
    {
        return typeof(ChaseState);
    }
}
