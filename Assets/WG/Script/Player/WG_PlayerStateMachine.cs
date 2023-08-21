using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WG_PlayerStateMachine
{
    public WG_PlayerState currentState { get; private set; }

    public void Initialize(WG_PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(WG_PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
