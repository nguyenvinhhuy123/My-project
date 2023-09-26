using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : BaseMovementState
{
    public WallSlideState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerWallSlide";
    }
    public override void OnEnter()
    {
        SetGravityScale(_machine._data.m_gravityScale*_machine._data.m_wallSlidingGravityMultiplier);
        base.OnEnter();
        OnSlideDown();
    }
    public override void OnUpdate()
    {
        OnSlideDown();
        base.OnUpdate();
    }
    public override void OnMovement()
    {

    }
    private void OnSlideDown()
    {
        _machine._reusableProperty.m_rigidBody2D.AddForce( _machine._data.m_wallSlidingSpeed*Vector2.down, ForceMode2D.Force);
        if (_machine._reusableProperty.m_rigidBody2D.velocity.y >  _machine._data.m_wallSlidingSpeed)
        _machine._reusableProperty.m_rigidBody2D.velocity = new Vector2(0f,  _machine._data.m_wallSlidingSpeed);
    }
    public override void StateCondition()
    {
        //Debug.Log(_machine._sharedData.MovementInput + " and " +  _machine._sharedData.WallContactDirection.x);
        if (!OnWall())
        {
            Debug.Log($"not on wall");
            _machine.OnChangeState(_machine.m_fall);
            return;
        }
        if (OnWall() && _machine._sharedData.MovementInput
        != _machine._sharedData.WallContactDirection.x)
        {
            Debug.Log($"input key != contact vector");
            _machine.OnChangeState(_machine.m_fall);
            return;
        }
        if (OnGround())
        {
            _machine.OnChangeState(_machine.m_idle);
        }
    }
    public override void SpriteFlip()
    {
        if (_machine._sharedData.WallContactDirection.x == 0f) return;
        if (_machine._sharedData.WallContactDirection.x > 0f ) 
        {
            _machine._reusableProperty.m_spriteRenderer.flipX = false;
            return;
        }
        if (_machine._sharedData.WallContactDirection.x < 0f ) 
        {
            _machine._reusableProperty.m_spriteRenderer.flipX = true;
            return;
        }
    }
}
