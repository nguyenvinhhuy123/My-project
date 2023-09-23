using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
public class GroundState : BaseMovementState
{
    public GroundState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
        _machine._sharedData.CanDoubleJump = true;
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
        _machine._sharedData.LastOnGroundTime = _data.m_coyoteTime;
    }
    public override void OnMovement()
    {
        base.OnMovement();
    }
    public override void StateCondition()
    {
        if (_machine._sharedData.OnJumpPressBufferTime > 0f)
        {
            _machine.OnChangeState(_machine.m_jump);
            return;
        }
        base.StateCondition();
    }
}
