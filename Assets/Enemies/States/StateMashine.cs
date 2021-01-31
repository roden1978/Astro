using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMashine : MonoBehaviour
{
    private Dictionary<System.Type, BaseState> _availableStates;
    public BaseState CurrentState { get; private set; }
    public BaseState PrevState { get; private set; }
    
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<System.Type, BaseState> states)
    {
        _availableStates = states;
    }
    
    void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState.GetType() != CurrentState?.GetType())
        {
            PrevState = _availableStates[CurrentState?.GetCurrentStateType()];
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(System.Type nextState)
    {
        CurrentState = _availableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }

    public System.Type GetPrevState
    {
        get => PrevState.GetType();
    }
    
}
