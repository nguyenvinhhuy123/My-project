using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AirborneState
{
    public FallState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerFall";
    }
    public override void OnEnter()
    {
        
        SetGravityScale(_machine._data.m_gravityScale* _machine._data.m_fallHangGravityMultiplier);
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
        if (Mathf.Abs(_machine._reusableProperty.m_rigidBody2D.velocity.y) > _machine._data.m_fallHangThreshold)
        SetGravityScale(_machine._data.m_gravityScale* _machine._data.m_fallGravityMultiplier);
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        _machine._reusableProperty.m_rigidBody2D.velocity = new Vector2(_machine._reusableProperty.m_rigidBody2D.velocity.x,
                        Mathf.Max(_machine._reusableProperty.m_rigidBody2D.velocity.y, -_machine._data.m_maxFallSpeed));
    }
    public override void OnMovement()
    {
        base.OnMovement();
    }
    public override void StateCondition()
    {
        if (OnGround())
        {
            if (_machine._reusableProperty.m_rigidBody2D.velocity.x == 0f)
            {
                _machine.OnChangeState(_machine.Idle);
                return;
            }
            if (_machine._reusableProperty.m_rigidBody2D.velocity.x != 0f)
            {
                _machine.OnChangeState(_machine.Run);
                return;
            }
        }
        if (_machine._sharedData.LastOnWallTime > 0f && _machine._sharedData.OnJumpPressBufferTime > 0f)
        {
            _machine.OnChangeState(_machine.WallJump);
            return;
        }
        if (_machine._sharedData.LastOnGroundTime > 0f && _machine._sharedData.OnJumpPressBufferTime > 0f)
        {
            _machine.OnChangeState(_machine.Jump);
            return;
        }
        base.StateCondition();
    }

}
