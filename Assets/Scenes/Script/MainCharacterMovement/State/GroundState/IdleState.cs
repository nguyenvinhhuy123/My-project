using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerIdle";
    }
    public override void OnEnter()
    {
        
    }   
    public override void OnExit()
    {

    }
    public override void OnInputHandle()
    {

    }
    public override void OnUpdate()
    {

    }
    public override void OnFixedUpdate()
    {

    }
}
