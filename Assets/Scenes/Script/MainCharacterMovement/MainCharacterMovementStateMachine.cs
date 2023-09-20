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
    public MainCharacterMovementStateMachine(MainCharacterMovementController controller)
    {
        _controller = controller;
        _reusableProperty = _controller._reusableProperty;
        _data = _controller.Data;
    }
}
