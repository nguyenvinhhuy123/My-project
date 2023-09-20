using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class BaseMovementState : IState
{
    protected static string ANIMATION_PARAM;
    protected MainCharacterMovementStateMachine _machine;
    protected readonly MainCharacterData _data;
    protected ReusableProperty _reusableProperty;
    public BaseMovementState(MainCharacterMovementStateMachine machine)
    {
        _machine = machine;
        _data = _machine._data;
        _reusableProperty = _machine._reusableProperty;
    }
    public virtual void OnEnter()
    {

    }   
    public virtual void OnExit()
    {

    }
    public virtual void OnInputHandle()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnFixedUpdate()
    {
        OnGround();
        OnWall();
        Debug.Log(_machine._sharedData.LastOnGroundTime = _data.m_coyoteTime);
    }
    protected bool OnGround()
    {
        RaycastHit2D[] CastHit = new RaycastHit2D[5];
        ContactFilter2D GroundFilter = new ContactFilter2D();
        LayerMask GroundLayer = LayerMask.GetMask("Ground");
        GroundFilter.SetLayerMask(GroundLayer);
        _reusableProperty.m_collider2D.Cast(Vector2.down, GroundFilter, CastHit, 0.05f);
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
                _machine._sharedData.LastOnGroundTime = _data.m_coyoteTime;
                return true;
            }
        }
        if (_reusableProperty.m_collider2D.Cast(Vector2.right, GroundFilter, CastHit, 0.05f) > 0) 
        {
            if (CastHit[0])
            {
                _machine._sharedData.WallContactDirection = Vector2.right;
                _machine._sharedData.LastOnGroundTime = _data.m_coyoteTime;
                return true;
            }
        }
        return false;
    }
    public virtual void PlayAnimation()
    {
        _machine._reusableProperty.m_animation.Play(ANIMATION_PARAM);
    }
}
