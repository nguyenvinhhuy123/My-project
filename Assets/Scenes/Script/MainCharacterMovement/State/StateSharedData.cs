using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSharedData
{
    public int MovementInput {get;set;}

    public float XAxisAccel {get; set;}

    public float YAxisAccel {get; set;}
    public bool CanDoubleJump {get;set;}

    public Vector2 WallContactDirection {get; set;}
    
    #region Timer System
    public float LastOnGroundTime {get; set;}
    public float LastOnWallTime {get; set;}
    public float OnJumpPressBufferTime {get; set;}
    #endregion
}
