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
    public IdleState m_idle;
    public RunState m_run;
    public JumpState m_jump;
    public FallState m_fall;
    #endregion
    public MainCharacterMovementStateMachine(MainCharacterMovementController controller)
    {
        Debug.Log("init state machine");
        _controller = controller;
        _reusableProperty = _controller._reusableProperty;
        _data = _controller.Data;
        _sharedData = new StateSharedData();
        #region State Initialization
        StateInit();
        #endregion
    }
    private void StateInit()
    {
        m_idle = new IdleState(this);
        m_run = new RunState(this);
        m_jump = new JumpState(this);
        m_fall = new FallState(this);
    }
}
