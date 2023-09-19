using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine : MonoBehaviour
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
