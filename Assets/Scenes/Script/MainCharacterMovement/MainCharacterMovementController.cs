using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

public class MainCharacterMovementController : MonoBehaviour
{
    [SerializeField] private MainCharacterData m_data;
    public MainCharacterData Data {get {return m_data;} }
    public ReusableProperty _reusableProperty {get; private set;}
    private MainCharacterMovementStateMachine _stateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        _reusableProperty = new ReusableProperty(gameObject);
        _stateMachine = new MainCharacterMovementStateMachine(this);
    }
    void Start()
    {
        _stateMachine.OnChangeState(_stateMachine.m_idle);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.OnUpdate();
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
        if (context.performed)
        {
            Debug.Log(context);
            _stateMachine._sharedData.IsFastFallPress = false;
        }
    }

}
