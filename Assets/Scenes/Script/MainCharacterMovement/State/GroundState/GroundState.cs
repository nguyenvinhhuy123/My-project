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
        SetGravityScale(_machine._data.m_gravityScale);
        base.OnEnter();
        _machine._sharedData.CanDoubleJump = true;
    }   
    public override void OnExit()
    {
        _machine._sharedData.IsFastFallPress = false;
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
        if (_machine._sharedData.OnJumpPressBufferTime > 0.01f)
        {
            _machine.OnChangeState(_machine.Jump);
            return;
        }
        if (_machine._reusableProperty.m_rigidBody2D.velocity.y < -0.01f)
        {
            _machine.OnChangeState(_machine.Fall);
            return;
        }
        base.StateCondition();
    }
}
