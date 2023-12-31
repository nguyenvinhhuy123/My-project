using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSharedData
{
    public float MovementInput {get;set;}
    public bool IsFastFallPress {get; set;}
    public bool CanDoubleJump {get;set;}
    public Vector2 WallContactDirection {get; set;}

    #region Timer System
    public float LastOnGroundTime {get; set;}
    public float LastOnWallTime {get; set;}
    public float OnJumpPressBufferTime {get; set;}
    public float OnWallJumpMovementRestrictedTime {get; set;}
    #endregion
}
