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
        if (Vector2.Distance(transform.position, _slime.player.transform.position) > _slime.SlimeData.StopChaseDistance)
        {
            return typeof(PatrolState);
        }

        if (_slime.PlayerSideDetect(transform) != _slime.MovingRight)
        {
            return typeof(FlipState);
        }
               
        if (Vector2.Distance(transform.position, _slime.player.transform.position) < _slime.SlimeData.AttackDistance)
        {
            return typeof(AttackState);
        }
        
        return null;
    }

    public override void FixedTick()
    {
        float maxVelocity = _slime.SlimeData.GetMaxVelocity;
        
        if (Mathf.Abs(rigidbody2D.velocity.x) < maxVelocity + maxVelocity / 2)
        {
            if (_slime.MovingRight)
            {
                rigidbody2D.AddForce(Vector2.right * _slime.SlimeData.Force, ForceMode2D.Force);
            }
            else
            {
                rigidbody2D.AddForce(Vector2.left * _slime.SlimeData.Force, ForceMode2D.Force);
            }
        }
    }


    public override System.Type GetCurrentStateType()
    {
        return typeof(ChaseState);
    }
}
