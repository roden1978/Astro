using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    
    private bool movingRight = true;
    public GameObject player { get; private set; }

    public StateMashine StateMashine => GetComponent<StateMashine>();
    private Dictionary<System.Type, BaseState> states;

    private void Awake()
    {
        InitializeStateMashine();
    }


    private void InitializeStateMashine()
    {
         states = new Dictionary<System.Type, BaseState>()
        {
            {typeof(PatrolState), new PatrolState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(FlipState), new FlipState(this)}
        };
        
        GetComponent<StateMashine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    public bool MovingRight
    {
        get => movingRight;
        set => movingRight = value;
    }
}
