using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : BaseMovementState
{
    public DamageState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerDamage";
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }   
}
