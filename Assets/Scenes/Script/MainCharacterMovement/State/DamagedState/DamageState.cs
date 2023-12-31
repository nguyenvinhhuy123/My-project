using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : BaseState
{
    public DamageState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerDamage";
    }
    public override void OnEnter()
    {
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
        base.OnMovement();
    }
    public override void StateCondition()
    {
        base.StateCondition();
    }
}
