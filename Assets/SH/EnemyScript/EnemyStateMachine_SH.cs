using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine_SH
{
    public EnemyState_SH currentState { get; private set; }


    public void Initialize(EnemyState_SH _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState_SH _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }

}
