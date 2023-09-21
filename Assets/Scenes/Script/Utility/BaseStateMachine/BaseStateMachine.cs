using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
public abstract class BaseStateMachine
{
    protected IState currentState;
    
    public void OnChangeState(IState nextState)
    {
        currentState?.OnExit();
        currentState = nextState;
        currentState?.OnEnter();
    }
    public void OnInputHandle()
    {
        currentState?.OnInputHandle();
    }
    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }
    public void OnFixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
}
}