using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnState : BaseState
{
    private float _reSpawnStateDurationTimer;
    public RespawnState(MainCharacterMovementStateMachine _machine) : base(_machine)
    {
        ANIMATION_PARAM = "PlayerSpawn";
    }
    public override void OnEnter()
    {
        _reSpawnStateDurationTimer = _machine._data.m_reSpawnDuration;
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        _reSpawnStateDurationTimer -= Time.deltaTime;
    }
    public override void SpriteFlip()
    {
        //*Keep empty instead of calling base */
    }
    public override void StateCondition()
    {
        if (_reSpawnStateDurationTimer > 0f) return;
        _machine.OnChangeState(_machine.Idle);
    }
}
