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
        //Debug.Log(GetType().Name + " entered");
    }   
    public virtual void OnExit()
    {
        //Debug.Log(GetType().Name + " exited");
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
    public virtual void SpriteFlip()
    {
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x == 0f) return;
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x > 0.01f ) 
        {
            _machine._reusableProperty.m_spriteRenderer.flipX = false;
            return;
        }
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x < -0.01f ) 
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
        _machine._sharedData.StayInDamagedStateTime -= Time.deltaTime;
    }
    protected Vector2 GetFacingDirection()
    {
        float facingX = (_machine._reusableProperty.m_spriteRenderer.flipX ? -1 : 1);
        Vector2 facingDirection = new Vector2(facingX, 0);
        return facingDirection;
    }
}
