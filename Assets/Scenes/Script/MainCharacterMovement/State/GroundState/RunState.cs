using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
using System;

public class RunState : GroundState
{
    public RunState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerRun";
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
    public override void OnMovement()
    {
        base.OnMovement();
    }
    public override void StateCondition()
    {
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x > -0.01f
        && _machine._reusableProperty.m_rigidBody2D.velocity.x < 0.01f
        && _machine._sharedData.MovementInput == 0)
        {
            _machine.OnChangeState(_machine.Idle);
            return;
        }
        base.StateCondition();
    }
}
