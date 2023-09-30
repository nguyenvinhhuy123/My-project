using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : AirborneState
{
    private Vector2 _lastWallContactDirection;
    public WallJumpState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerJump";
    }
    public override void OnEnter()
    {
        SetGravityScale(_machine._data.m_gravityScale);
        base.OnEnter();
        _machine._sharedData.OnJumpPressBufferTime = 0f;
        _machine._sharedData.LastOnWallTime = 0f;
        _machine._sharedData.OnWallJumpMovementRestrictedTime = _machine._data.m_wallJumpMovementConstraintTime;
        _lastWallContactDirection = _machine._sharedData.WallContactDirection;
        OnWallJump();
    }
    private void OnWallJump()
    {
        float targetUpForce = _machine._data.m_wallJumpForce;
        float targetSideForce = _machine._data.m_wallJumpInitializeSidedForce;
        targetUpForce -= (_machine._reusableProperty.m_rigidBody2D.velocity.y < 0 
                        ? _machine._reusableProperty.m_rigidBody2D.velocity.y : 0);
        Vector2 targetForce = targetUpForce*Vector2.up +(-1*targetSideForce*_lastWallContactDirection);
        _machine._reusableProperty.m_rigidBody2D.AddForce(targetForce, ForceMode2D.Impulse);
    }
    public override void OnMovement()
    {
        float targetSpeed = _machine._sharedData.MovementInput * _machine._data.m_runMaxSpeed;
        float accel;
        if (_machine._sharedData.OnWallJumpMovementRestrictedTime > 0f
        && Mathf.Sign(_machine._sharedData.MovementInput) == Mathf.Sign(_lastWallContactDirection.x)
        )
        {
            //*We only want to constrains input if the input is against the initial force Vector
            //*aka same direction with wall contact direction
            //*This will help player to have more momentum went they want to jump out of the wall, thus create for freedom in movement
            targetSpeed = targetSpeed * _machine._data.m_wallJumpMovementConstraintPercentile;
        }
        targetSpeed += -1*_machine._data.m_wallJumpInitializeSidedForce*_lastWallContactDirection.x;

        Debug.Log($"targetSpeed: {targetSpeed}");
        accel = (Mathf.Abs(targetSpeed) > 0.01f) ? _machine._data.m_realAccel : _machine._data.m_realDeccel;    

        //Whenever our player moves faster than our maxSpeed (due to speed buff/push mechanism), 
        //We do not want to reduce our player Speed to the Clamp so fast
        //This give player a chance to create a hyper speed boost situation more freely
        if (
            (Mathf.Abs(_machine._reusableProperty.m_rigidBody2D.velocity.x) > Mathf.Abs(targetSpeed))
        &&  (Mathf.Sign(_machine._reusableProperty.m_rigidBody2D.velocity.x) == Mathf.Sign(targetSpeed))
        && (Mathf.Abs(targetSpeed) > 0.01f)
        )
        {
            accel *= _machine._data.HyperSpeedDeccelMultiplier;
            Debug.Log("Hyper Speed");
        }
        
        float amountToReachTargetSpeed = targetSpeed - _machine._reusableProperty.m_rigidBody2D.velocity.x;

        float realForceToAdd = amountToReachTargetSpeed * accel;
        Debug.Log(realForceToAdd + " added");
        _machine._reusableProperty.m_rigidBody2D.AddForce(realForceToAdd*Vector2.right, ForceMode2D.Force);
    }
    public override void StateCondition()
    {
        if (_machine._reusableProperty.m_rigidBody2D.velocity.y <= 0f)
        {
            //TODO: Change to fall state
            _machine.OnChangeState(_machine.m_fall);
            return;
        }
        base.StateCondition();
    }
}
