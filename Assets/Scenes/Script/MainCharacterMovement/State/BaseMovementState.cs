using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
public class BaseMovementState : BaseState
{
    public BaseMovementState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
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
        OnMovement();
        
    }
    public virtual void OnMovement()
    {
        float targetSpeed = _machine._sharedData.MovementInput * _machine._data.m_runMaxSpeed;

        float accel;
        accel = (Mathf.Abs(targetSpeed) > 0.01f) ? _machine._data.m_realAccel : _machine._data.m_realDeccel;    

        //Whenever our player moves faster than our maxSpeed (due to speed buff/push mechanism), 
        //We do not want to reduce our player Speed to the Clamp so fast
        //This give player a chance to create a hyper speed boost situation more freely
        if (
            (Mathf.Abs(_machine._reusableProperty.m_rigidBody2D.velocity.x) > Mathf.Abs(targetSpeed))
        &&  (Mathf.Sign(_machine._reusableProperty.m_rigidBody2D.velocity.x) == Mathf.Sign(targetSpeed))
        && (MathF.Abs(targetSpeed) > 0.01f)
        )
        {
            accel *= _machine._data.HyperSpeedDeccelMultiplier;
            Debug.Log("Hyper Speed");
        }
        float amountToReachTargetSpeed = targetSpeed - _machine._reusableProperty.m_rigidBody2D.velocity.x;

        float realForceToAdd = amountToReachTargetSpeed * accel;

        _machine._reusableProperty.m_rigidBody2D.AddForce(realForceToAdd*Vector2.right, ForceMode2D.Force);
    }
    public override void StateCondition()
    {
        base.StateCondition();
    }
}
