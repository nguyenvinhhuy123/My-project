using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
public abstract class BaseStateMachine
{
    protected IState currentState;
    
    void OnChangeState(IState nextState)
    {
        currentState?.OnExit();
        currentState = nextState;
        currentState?.OnEnter();
    }
    void OnInputHandle()
    {
        currentState?.OnInputHandle();
    }
    void OnUpdate()
    {
        currentState?.OnUpdate();
    }
    void OnFixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
}
}