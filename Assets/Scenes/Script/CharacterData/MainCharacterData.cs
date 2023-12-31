using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


[CreateAssetMenu(menuName = "My project/MainCharacterData")]
public class MainCharacterData : ScriptableObject
{
    [Header("Gravity manipulation data")]
    [HideInInspector] public float m_gravityStrength;
    [HideInInspector] public float m_gravityScale;

    [HideInInspector] public float m_doubleJumpGravityStrength;
    [HideInInspector] public float m_doubleJumpGravityScale;

    [HideInInspector] public float m_wallJumpGravityStrength;
    [HideInInspector] public float m_wallJumpGravityScale;

    [Header("Running related data")]
    [Range(0f, 1f)]public float HyperSpeedDeccelMultiplier;
    public float m_runMaxSpeed;
    public float m_runAccel;
    public float m_runDeccel;
    [HideInInspector] public float m_realAccel;
    [HideInInspector] public float m_realDeccel;
    [Header("Jumping related data")]
    public float m_hangTimeThreshold;
    [Range (0f, 1f)]public float m_jumpHangGravityMultiplier;
    public float m_jumpHeight;
    public float m_jumpTimeToApex;
    [HideInInspector] public float m_jumpForce;
    [Header("DoubleJumping related data")]
    public float m_doubleJumpHeight;
    public float m_doubleJumpTimeToApex;
    [HideInInspector]public float m_doubleJumpForce;

    [Header("WallJumping related data")]
    public float m_wallJumpHeight;
    public float m_wallJumpTimeToApex;
    [HideInInspector] public float m_wallJumpForce;

    [Tooltip("Initial side force for wall jump")]
    public float m_wallJumpInitializeSidedForce;
    [Range(0f, 1f)]public float m_wallJumpMovementConstraintTime;
    [Range(0f, 1f)] public float m_wallJumpMovementConstraintPercentile;
    
    [Header("Fast fall related data")]
    public float m_maxFastFallSpeed;
    public float m_fastFallGravityMultiplier;

    [Header("Falling related data")]
    public float m_maxFallSpeed;
    public float m_fallHangThreshold;
    [Range(0f,1f)]public float m_fallHangGravityMultiplier;
    public float m_fallGravityMultiplier;
    [Header("Wall Sliding related data")]
    public float m_wallSlidingSpeed;
    [Tooltip("Wall Sliding gravity multi should be set to 0 or small amount")]
    [Range(0f, 0.1f)]public float m_wallSlidingGravityMultiplier;
    public float m_wallSlidingAccel;
    [Header("Get hit related Data")]
    public Vector2 m_knockBackForce;
    [Range(0.5f, 1f)] public float m_DamagedStateDuration;
    [Header("Dead and Respawn")]
    [Range(0.5f, 1.5f)] public float m_DeadStateDuration;
    public float m_reSpawnDuration;

    [Header("Assisted variables")]
    [Range(0.01f,0.75f)]public float m_coyoteTime;
    [Range(0.01f, 0.75f)]public float m_jumpInputBufferTime;

    [Space(2f)]
    [Header("VFX Effect")]
    public GameObject m_fastFallVFXObject;

    [Header("SFX Effect")]
    public AudioClip  m_jumpSFXEffect;

    public 
    /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        m_runAccel = Mathf.Clamp(m_runAccel, 0.01f, m_runMaxSpeed);
        m_runDeccel = Mathf.Clamp(m_runDeccel, 0.01f, m_runMaxSpeed);

        m_gravityStrength = -(2*m_jumpHeight) / (m_jumpTimeToApex* m_jumpTimeToApex);
        m_gravityScale = m_gravityStrength / Physics2D.gravity.y;

        m_doubleJumpGravityStrength = -(2*m_doubleJumpHeight) / (m_doubleJumpTimeToApex* m_doubleJumpTimeToApex);
        m_doubleJumpGravityScale = m_doubleJumpGravityStrength / Physics2D.gravity.y;

        m_wallJumpGravityStrength = -(2*m_wallJumpHeight) / (m_wallJumpTimeToApex* m_wallJumpTimeToApex);
        m_wallJumpGravityScale = m_wallJumpGravityStrength / Physics2D.gravity.y;

        //Calculate are run acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / runMaxSpeed
		m_realAccel = (1/Time.fixedDeltaTime * m_runAccel) / m_runMaxSpeed;
		m_realDeccel = (1/Time.fixedDeltaTime * m_runDeccel) / m_runMaxSpeed;

        m_jumpForce = Mathf.Abs(m_gravityStrength)*m_jumpTimeToApex;

        m_doubleJumpForce = Mathf.Abs(m_doubleJumpGravityStrength)*m_doubleJumpTimeToApex;

        m_wallJumpForce = Mathf.Abs(m_wallJumpGravityStrength)*m_wallJumpTimeToApex;

    }
}
