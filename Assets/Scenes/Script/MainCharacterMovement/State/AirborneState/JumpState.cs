using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : AirborneState
{
    public JumpState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerJump";
    }
    public override void OnEnter()
    {
        SetGravityScale(_machine._data.m_gravityScale);
        base.OnEnter();
        _machine._sharedData.OnJumpPressBufferTime = 0f;
        _machine._sharedData.LastOnGroundTime = 0f;
        OnJump();
        AudioManager.Instance.PlaySFX(_machine._data.m_jumpSFXEffect);
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
        if (Mathf.Abs(_machine._reusableProperty.m_rigidBody2D.velocity.y) < _machine._data.m_hangTimeThreshold)
        {
            SetGravityScale(_machine._data.m_gravityScale*_machine._data.m_jumpHangGravityMultiplier);
        }
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    public override void OnMovement()
    {
        base.OnMovement();
    }
    public override void StateCondition()
    {
        if (_machine._reusableProperty.m_rigidBody2D.velocity.y <= 0f)
        {
            //TODO: Change to fall state
            _machine.OnChangeState(_machine.Fall);
            return;
        }
        base.StateCondition();
    }
    private void OnJump()
    {
        float targetForce = _machine._data.m_jumpForce;
        targetForce -= (_machine._reusableProperty.m_rigidBody2D.velocity.y < 0 
                        ? _machine._reusableProperty.m_rigidBody2D.velocity.y : 0);
        _machine._reusableProperty.m_rigidBody2D.AddForce(targetForce * Vector2.up, ForceMode2D.Impulse);
    }
}
