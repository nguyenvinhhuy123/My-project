using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MainCharacterMovementStateMachine : BaseStateMachine
{
    public MainCharacterMovementController _controller {get;}
    public ReusableProperty _reusableProperty {get;}
    public MainCharacterData _data {get;}
    public StateSharedData _sharedData {get;}

    #region State Register
    public IdleState Idle {get; private set;}
    public RunState Run {get; private set;}
    public JumpState Jump {get; private set;}
    public FallState Fall {get; private set;}
    public DoubleJumpState DoubleJump {get; private set;}
    public FastFallState FastFall {get; private set;}
    public WallSlideState WallSlide {get; private set;}
    public WallJumpState WallJump {get; private set;}
    public DamageState Damaged {get; private set;}
    #endregion
    public MainCharacterMovementStateMachine(MainCharacterMovementController controller)
    {
        Debug.Log("init state machine");
        _controller = controller;
        _reusableProperty = _controller.ReusableProperty;
        _data = _controller.Data;
        _sharedData = new StateSharedData();
        #region State Initialization
        StateInit();
        #endregion
    }
    private void StateInit()
    {
        Idle = new IdleState(this);
        Run = new RunState(this);
        Jump = new JumpState(this);
        Fall = new FallState(this);
        DoubleJump = new DoubleJumpState(this);
        FastFall = new FastFallState(this);
        WallSlide = new WallSlideState(this);
        WallJump = new WallJumpState(this);
        Damaged = new DamageState(this);
    }
    /// <summary>
    /// callback function on character is dead
    /// </summary>
    public void OnDead()
    {
        Debug.Log("Not Implemented " + nameof(OnDead));
    }
    /// <summary>
    /// Callback function on character damaged
    /// </summary>
    public void OnDamage()
    {
        OnChangeState(Damaged);
    }
    /// <summary>
    /// Callback function on character spawn back to world
    /// </summary>
    public void OnSpawn()
    {
        Debug.Log("Not Implemented " + nameof(OnSpawn));
    }
}
