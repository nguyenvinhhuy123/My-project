using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirborneState : BaseMovementState
{
    public AirborneState(MainCharacterMovementStateMachine _machine) : base(_machine){}
    public override void OnEnter()
    {
        _machine._sharedData.LastOnGroundTime = 0f;
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
        if (_machine._sharedData.IsFastFallPress)
        {
            //TODO: Change to fast fall state
            _machine.OnChangeState(_machine.FastFall);
            return;
        }
        if (_machine._sharedData.CanDoubleJump
        && _machine._sharedData.OnJumpPressBufferTime > 0f)
        {
            _machine.OnChangeState(_machine.DoubleJump);
            return;
        }
        if (OnWall() 
        && Mathf.Sign(_machine._sharedData.MovementInput) 
        == Mathf.Sign(_machine._sharedData.WallContactDirection.x)
        )
        {
            _machine.OnChangeState(_machine.WallSlide);
            return;
        }
        base.StateCondition();
        
    }
}
