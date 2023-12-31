using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.InputSystem;
public class BaseState : IState
{
    protected string ANIMATION_PARAM;
    protected MainCharacterMovementStateMachine _machine;
    protected readonly MainCharacterData _data;
    protected ReusableProperty _reusableProperty;
    public BaseState(MainCharacterMovementStateMachine machine)
    {
        _machine = machine;
        _data = _machine._data;
        _reusableProperty = _machine._reusableProperty;
    }
    #region Virtual Callback 
    public virtual void OnEnter()
    {
        PlayAnimation();
        Debug.Log(GetType().Name + " entered");
    }   
    public virtual void OnExit()
    {
        Debug.Log(GetType().Name + " exited");
    }
    public virtual void OnInputHandle()
    {
        
    }
    public virtual void OnUpdate()
    {
        SpriteFlip();
        TimerChange();
    }
    public virtual void OnFixedUpdate()
    {
        StateCondition();
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
    public virtual void StateCondition()
    {

    }
    #endregion
    protected bool OnGround()
    {
        RaycastHit2D[] CastHit = new RaycastHit2D[5];
        ContactFilter2D GroundFilter = new ContactFilter2D();
        LayerMask GroundLayer = LayerMask.GetMask("Ground");
        GroundFilter.SetLayerMask(GroundLayer);
        _reusableProperty.m_collider2D.Cast(Vector2.down, GroundFilter, CastHit, 0.015f);
        if (CastHit[0])
        {
            _machine._sharedData.LastOnGroundTime = _data.m_coyoteTime;
            return true;
        }
        return false;
    }
    protected bool OnWall()
    {
        RaycastHit2D[] CastHit = new RaycastHit2D[5];
        ContactFilter2D GroundFilter = new ContactFilter2D();
        LayerMask GroundLayer = LayerMask.GetMask(TagStorage.GROUND_LAYER_MASK);
        GroundFilter.SetLayerMask(GroundLayer);
        if (_reusableProperty.m_collider2D.Cast(Vector2.left, GroundFilter, CastHit, 0.05f) > 0) 
        {
            if (CastHit[0])
            {
                _machine._sharedData.WallContactDirection = Vector2.left;
                _machine._sharedData.LastOnWallTime = _data.m_coyoteTime;
                return true;
            }
        }
        if (_reusableProperty.m_collider2D.Cast(Vector2.right, GroundFilter, CastHit, 0.05f) > 0) 
        {
            if (CastHit[0])
            {
                _machine._sharedData.WallContactDirection = Vector2.right;
                _machine._sharedData.LastOnWallTime = _data.m_coyoteTime;
                return true;
            }
        }
        _machine._sharedData.WallContactDirection = Vector2.zero;
        return false;
    }
    public virtual void PlayAnimation()
    {
        _machine._reusableProperty.m_animator.Play(ANIMATION_PARAM);
    }
    private void SpriteFlip()
    {
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x == 0f) return;
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x > 0f ) 
        {
            _machine._reusableProperty.m_spriteRenderer.flipX = false;
            return;
        }
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x < 0f ) 
        {
            _machine._reusableProperty.m_spriteRenderer.flipX = true;
            return;
        }
    }
    protected void SetGravityScale(float scale)
    {
        _machine._reusableProperty.m_rigidBody2D.gravityScale = scale;
    }
    private void TimerChange()
    {
        _machine._sharedData.LastOnGroundTime -= Time.deltaTime;   
        _machine._sharedData.LastOnWallTime -= Time.deltaTime;   
        _machine._sharedData.OnJumpPressBufferTime -= Time.deltaTime;   
        _machine._sharedData.OnWallJumpMovementRestrictedTime -= Time.deltaTime;
    }
    protected Vector2 GetFacingDirection()
    {
        float facingX = (_machine._reusableProperty.m_spriteRenderer.flipX ? -1 : 1);
        Vector2 facingDirection = new Vector2(facingX, 0);
        return facingDirection;
    }

}
