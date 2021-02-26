using UnityEngine;

public class JumpState : BaseState
{
    private Player _player;
    private System.Type _prevState;
    
    public JumpState(Player player) : base(player.gameObject)
    {
        _player = player;
    }

    public override System.Type Tick()
    {
        if (_prevState == null) _prevState = _player.GetComponent<StateMashine>().GetPrevState;
        
        if (_player.GroundCollider2D.GroundCheck("Ground"))
        {
            //Debug.Log("Jump");
            Vector2 force = Vector2.up * _player.JumpForce;
            rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            return _prevState;
        }

        return null;
    }

    public override void FixedTick()
    {
        /*if (_player.GroundCollider2D.GroundCheck("Ground"))
        {
            Debug.Log("Jump");
            Vector2 force = Vector2.up * _player.JumpForce;
            rigidbody2D.AddForce(force, ForceMode2D.Impulse);
        }*/
            
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(JumpState);
    }
}
