using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Utilities;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(SpriteRenderer))]

public class MainCharacterController : MonoBehaviour
{
    [SerializeField] private MainCharacterData m_data;
    public MainCharacterData Data {get {return m_data;} }
    public ReusableProperty ReusableProperty {get; private set;}
    private MainCharacterMovementStateMachine _stateMachine;
    private UnityAction<int,bool> _onDamageAction;

    // Start is called before the first frame update
    void Awake()
    {
        ReusableProperty = new ReusableProperty(gameObject);
        _stateMachine = new MainCharacterMovementStateMachine(this);
        _onDamageAction += OnDamaged;
    }
    void Start()
    {
        ReusableProperty.m_damageable.EventListenerRegister(_onDamageAction);
        OnSpawn();
    }
    void OnDestroy()
    {
        ReusableProperty.m_damageable.EventListenerRegister(_onDamageAction);
        _onDamageAction -= OnDamaged;
        GameManager.Instance.OnCharacterDestroy();
    }
    // Update is called once per frame
    void Update()
    {
        _stateMachine.OnUpdate();
        _stateMachine.OnInputHandle();
    }
    void FixedUpdate()
    {
        _stateMachine.OnFixedUpdate();
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        _stateMachine._sharedData.MovementInput = context.ReadValue<float>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log(context);
            _stateMachine._sharedData.OnJumpPressBufferTime = m_data.m_jumpInputBufferTime;
        }
    }
    public void OnFastFall(InputAction.CallbackContext context)
    {
        //*This ensured that IsFastFallPress is reset to false after events is performed,
        // *not after button is released
        //*This can later be change to a trigger event
        //*Or to make sure everything work as expected, Build a unique PlayerInput Component and InputEventSystem 
        //*To Include in State Machine

        //? TLDR Subject to change
        if (context.started)
        {
            _stateMachine._sharedData.IsFastFallPress = context.started;
        }
        // if (context.performed)
        // {
        //     Debug.Log(context);
        //     _stateMachine._sharedData.IsFastFallPress = false;
        // }
    }
    public void OnDamaged(int currentHealth, bool isDead)
    {
        if (isDead)
        {
            _stateMachine.OnChangeState(_stateMachine.Dead);
            return;
        }
        _stateMachine.OnChangeState(_stateMachine.Damaged);
    }
    public void OnSpawn()
    {
        _stateMachine.OnSpawn();
    }

}
