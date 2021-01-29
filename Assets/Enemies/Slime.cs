using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject player { get; private set; }

    public StateMashine StateMashine => GetComponent<StateMashine>();

    private void Awake()
    {
        InitializeStateMashine();
    }

    private void InitializeStateMashine()
    {
        Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
        
        GetComponent<StateMashine>().SetStates(states);
    }

    public void SetTarget(GameObject target)
    {
        player = target;
    }
}
