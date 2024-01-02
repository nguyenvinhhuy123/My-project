using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : BaseState
{
    private float _deadStateDurationTime; 
    public DeadState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerDead";
    }
    public override void OnEnter()
    {
        _deadStateDurationTime = _machine._data.m_DeadStateDuration;
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        _deadStateDurationTime -= Time.deltaTime;
    }
    public override void OnExit()
    {
        base.OnExit();
        GameObject.Destroy(_machine._controller.gameObject);
    }
    public override void OnMovement()
    {
        //*let empty instead of calling base OnMovement by default
    }
    public override void SpriteFlip()
    {
        //*Keep empty instead of calling base */
    }
    public override void StateCondition()
    {
        if (_deadStateDurationTime > 0f) return;
        OnExit();
    }
}