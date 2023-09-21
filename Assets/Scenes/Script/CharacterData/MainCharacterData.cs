using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "My project/MainCharacterData")]
public class MainCharacterData : ScriptableObject
{
    [Header("Gravity manipulation data")]
    [HideInInspector] public float m_gravityStrength;
    [HideInInspector] public float m_gravityScale;
    [Header("Running related data")]
    [Range(0f, 1f)]public float HyperSpeedDeccelMultiplier;
    public float m_runMaxSpeed;
    public float m_runAccel;
    public float m_runDeccel;
    [HideInInspector] public float m_realAccel;
    [HideInInspector] public float m_realDeccel;
    [Header("Jumping related data")]
    public float m_jumpHeight;
    public float m_jumpTimeToApex;
    [HideInInspector] public float m_jumpForce;
    [Header("DoubleJumping related data")]
    public float m_doubleJumpHeight;
    public float m_doubleJumpTimeToApex;
    [Header("WallJumping related data")]
    public float m_wallJumpHeight;
    public float m_wallJumpTimeToApex;
    [HideInInspector] public float m_wallJumpForce;
    [Tooltip("Initial side force for wall jump")]
    public float m_wallJumpInitializeSidedForce;
    [Range(0f, 1.5f)]public float m_wallJumpMovementConstraintTime;
    [Range(0f, 1f)] public float m_wallJumpMovementConstraintPercentile;
    [HideInInspector] public float m_doubleJumpForce;
    [Header("Jump gravity related data")]
    [Range (0f, 1f)]public float m_jumpHangGravityMultiplier;
    [Range (0f, 0.5f)]public float m_hangTimeThreshold;
    [Header("Falling related data")]
    public float m_maxFallSpeed;
    public float m_fallAccel;
    [Range(0f,0.5f)]public float m_fallHangThreshold;
    [Range(0f,1f)]public float m_fallHangGravityMultiplier;
    public float m_fallGravityMultiplier;
    [Header("Wall Sliding related data")]
    public float m_wallSlidingSpeed;
    [Tooltip("Wall Sliding gravity multi should be set to 0 or small amount")]
    [Range(0f, 0.1f)]public float m_wallSlidingGravityMultiplier;
    public float m_wallSlidingAccel;
    [Header("Assisted variables")]
    [Range(0.01f,0.75f)]public float m_coyoteTime;
    [Range(0.01f, 0.75f)]public float m_jumpInputBufferTime;
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        m_runAccel = Mathf.Clamp(m_runAccel, 0.01f, m_runMaxSpeed*10);
        m_runDeccel = Mathf.Clamp(m_runDeccel, 0.01f, m_runMaxSpeed*10);
        m_fallAccel = Mathf.Clamp(m_fallAccel,0.01f,m_maxFallSpeed);

        m_gravityStrength = -(2*m_jumpHeight) / (m_jumpTimeToApex* m_jumpTimeToApex);
        m_gravityScale = m_gravityStrength / Physics2D.gravity.y;
        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		m_realAccel = (1/Time.fixedDeltaTime * m_runAccel) / m_runMaxSpeed;
		m_realDeccel = (1/Time.fixedDeltaTime * m_runDeccel) / m_runMaxSpeed;

        m_jumpForce = Mathf.Abs(m_gravityStrength)*m_jumpTimeToApex;

    }
}
