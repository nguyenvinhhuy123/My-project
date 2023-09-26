using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallState : AirborneState
{
    private GameObject _fastFallVFXObject;
    public FastFallState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerFall";
    }
    public override void OnEnter()
    {
        SetGravityScale(_machine._data.m_gravityScale* _machine._data.m_fastFallGravityMultiplier);
        base.OnEnter();
        OnFastFall();
        _machine._sharedData.IsFastFallPress = false;
        _fastFallVFXObject = GameObject.Instantiate(_machine._data.m_fastFallVFXObject, _machine._controller.gameObject.transform);
    }
    public override void OnUpdate()
    {
        OnFastFall();
        base.OnUpdate();
    }
    private void OnFastFall()
    {
        float fastFallSpeed = _machine._data.m_maxFastFallSpeed;
        _machine._reusableProperty.m_rigidBody2D.velocity = 
        new Vector2(_machine._reusableProperty.m_rigidBody2D.velocity.x, -fastFallSpeed);
    }
    public override void OnExit()
    {
        GameObject.Destroy(_fastFallVFXObject);
    }
    public override void StateCondition()
    {
        if (OnGround())
        {
            if (_machine._reusableProperty.m_rigidBody2D.velocity.x == 0f)
            {
                _machine.OnChangeState(_machine.m_idle);
                return;
            }
            if (_machine._reusableProperty.m_rigidBody2D.velocity.x != 0f)
            {
                _machine.OnChangeState(_machine.m_run);
                return;
            }
        }
        base.StateCondition();
    }
    private void onVFXFlip()
    {
        if (_machine._reusableProperty.m_spriteRenderer.flipX)
        {
            _fastFallVFXObject.transform.localPosition = new Vector3(-0.05f, 0f, 0f);
        }
        else 
        {
            _fastFallVFXObject.transform.localPosition = new Vector3(0.05f, 0f, 0f);
        }
    }
    public override void SpriteFlip()
    {
        base.SpriteFlip();
        onVFXFlip();
    }
}
