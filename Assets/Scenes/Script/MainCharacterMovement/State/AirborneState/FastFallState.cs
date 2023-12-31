using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallState : AirborneState
{
    private ParticleSystem _fastFallVFX;
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
        _fastFallVFX =
        GameObject
        .Instantiate(_machine._data.m_fastFallVFXObject,
                    _machine._controller.gameObject.transform)
        .GetComponent<ParticleSystem>();
        _fastFallVFX.Play();
    }
    public override void OnUpdate()
    {
        OnFastFall();
        OnVFXFlip();
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
        _fastFallVFX.Stop();
        GameObject.Destroy(_fastFallVFX.gameObject);
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
        base.StateCondition();
    }
    public override void OnMovement()
    {
        _machine._reusableProperty.m_rigidBody2D.velocity = 
        new Vector2(0f, _machine._reusableProperty.m_rigidBody2D.velocity.y);
    }
    private void OnVFXFlip()
    {
        float facingX = GetFacingDirection().x;

            _fastFallVFX.transform.localPosition = new Vector3(0.05f*facingX, 0f, 0f);
    }

}
