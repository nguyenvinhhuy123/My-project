using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
