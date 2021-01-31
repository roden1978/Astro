using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipState : BaseState
{
    private Slime _slime;
    private System.Type _prevState;
    
    public FlipState(Slime slime) : base(slime.gameObject)
    {
        _slime = slime;
    }

    public override System.Type Tick()
    {
        _prevState = _slime.GetComponent<StateMashine>().GetPrevState;

        _slime.MovingRight = !_slime.MovingRight;
        transform.Rotate(0f, 180f, 0f);
        
        return _prevState;
    }

    public override System.Type GetCurrentStateType()
    {
        return typeof(FlipState);
    }
}
