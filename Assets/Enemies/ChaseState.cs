using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public ChaseState(Slime _slime) : base(_slime.gameObject)
    {
    }

    public override System.Type Tick()
    {
        throw new System.NotImplementedException();
    }
}
