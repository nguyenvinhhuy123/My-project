using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : BaseState
{
    public DamageState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerDamaged";
    }
    public override void OnEnter()
    {
        _machine._sharedData.StayInDamagedStateTime = _machine._data.m_DamagedStateDuration;
        OnKnockBack();
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
        //let empty instead of calling base OnMovement by default
    }
    private void OnKnockBack()
    {
        /*
        *Check facing direction
        *initial force = knockback x * facing dir , knockback y 
        */
        float knockBackXDirection = -GetFacingDirection().x;
        Vector2 targetKnockBackForce 
        = new Vector2(
            knockBackXDirection*_machine._data.m_knockBackForce.x, 
            _machine._data.m_knockBackForce.y);
        
        _machine._reusableProperty.m_rigidBody2D.velocity = targetKnockBackForce;
    }
    public override void StateCondition()
    {
        if (_machine._sharedData.StayInDamagedStateTime > 0f) return;
        if (!OnGround()) 
        {
            _machine.OnChangeState(_machine.Fall);
            return;
        }
        if (_machine._sharedData.MovementInput != 0f)
        {
            _machine.OnChangeState(_machine.Run);
        }
        if (_machine._reusableProperty.m_rigidBody2D.velocity.x == 0f
        && _machine._sharedData.MovementInput == 0)
        {
            _machine.OnChangeState(_machine.Idle);
            return;
        }
    }
    public override void SpriteFlip()
    {
        //*Keep empty instead of calling base */
    }
}
