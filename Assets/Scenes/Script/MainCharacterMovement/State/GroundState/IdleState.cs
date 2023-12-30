using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;

public class IdleState : GroundState
{
    public IdleState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerIdle";
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }   
    public override void OnExit()
    {
        base.OnExit();
    }
    public override void OnInputHandle()
    {
        base.OnInputHandle();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void StateCondition()
    {
        if (_machine._sharedData.MovementInput != 0f)
        {
            _machine.OnChangeState(_machine.m_run);
            return;
        }
        base.StateCondition();
    }
}
