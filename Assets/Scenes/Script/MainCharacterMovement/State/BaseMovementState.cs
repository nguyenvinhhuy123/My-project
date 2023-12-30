using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
public class BaseMovementState : BaseState
{
    public BaseMovementState(MainCharacterMovementStateMachine machine) : base(_machine)
    {
        _machine = machine;
        _data = _machine._data;
        _reusableProperty = _machine._reusableProperty;
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
        
        base.StateCondition();
    }
}
