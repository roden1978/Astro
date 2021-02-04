using UnityEngine;

public class AttackState : BaseState
{
    private Slime _slime;
    
    public AttackState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
    }

    public override System.Type Tick()
    {
        if (_slime.PlayerSideDetect(transform) != _slime.MovingRight)
        {
            return typeof(FlipState);
        }
		
        if (Vector2.Distance(transform.position, _slime.player.transform.position) > _slime.SlimeData.attackDistance)
        {
            return typeof(ChaseState);
        }
        
        return null;
    }

    public override void FixedTick()
    {
        if(_slime.CheckGround())
        {
            rigidbody2D.AddForce(Vector2.up * _slime.SlimeData.jumpForce, ForceMode2D.Impulse);
        }
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(AttackState);
    }
}
