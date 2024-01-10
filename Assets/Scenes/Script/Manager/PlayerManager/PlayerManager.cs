using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

public class PlayerManager : PersistenceSingleton<PlayerManager>
{
    private PlayerSpawner _spawner;
    private PlayerLifeCounter _lifeCounter;
    private MainCharacterController m_characterInstance;
    public MainCharacterController CharacterInstance {get{ return m_characterInstance;}}
    private UnityEvent m_onCharacterDestroyEvent;
    private UnityEvent<MainCharacterController> m_onCharacterSpawnEvent;
    private UnityEvent<CheckPointController> m_onCheckedEvent;
    protected override void Awake()
    {
        base.Awake();
        if (!gameObject.TryGetComponent<PlayerSpawner>(out _spawner))
        {
            _spawner = gameObject.AddComponent<PlayerSpawner>();
        }
        if (!gameObject.TryGetComponent<PlayerLifeCounter>(out _lifeCounter))
        {
            _lifeCounter = gameObject.AddComponent<PlayerLifeCounter>();
        }
        InitEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CharacterDestroy()
    {
        gameObject?.BroadcastMessage("OnCharacterDestroy", SendMessageOptions.DontRequireReceiver);
        m_onCharacterDestroyEvent.Invoke();
    }
    public void CharacterSpawn(MainCharacterController character)
    {  
        gameObject?.BroadcastMessage("OnCharacterSpawn", character, SendMessageOptions.DontRequireReceiver);
        m_onCharacterSpawnEvent.Invoke(character);
    }
    public void Checked(CheckPointController checkPoint)
    {
        gameObject?.BroadcastMessage("OnChecked", checkPoint, SendMessageOptions.DontRequireReceiver);
        m_onCheckedEvent.Invoke(checkPoint);
    }
    private void OnCharacterSpawn(MainCharacterController character)
    {
        m_characterInstance = character;
    }
    private void InitEvent()
    {
        m_onCharacterDestroyEvent = new UnityEvent();
        m_onCharacterSpawnEvent = new UnityEvent<MainCharacterController>();
        m_onCheckedEvent = new UnityEvent<CheckPointController>();
    }
    public void OnCharacterDestroyListenerRegister(UnityAction act){
        if (act == null) return;
        m_onCharacterDestroyEvent.AddListener(act);
    }
    public void OnCharacterDestroyListenerUnRegister(UnityAction act){
        if (act == null) return;
        m_onCharacterDestroyEvent.RemoveListener(act);
    }
    public void OnCharacterSpawnListenerRegister(UnityAction<MainCharacterController> act){
        if (act == null) return;
        m_onCharacterSpawnEvent.AddListener(act);
    }
    public void OnCharacterSpawnListenerUnRegister(UnityAction<MainCharacterController> act){
        if (act == null) return;
        m_onCharacterSpawnEvent.RemoveListener(act);
    }
    public void OnCheckedListenerRegister(UnityAction<CheckPointController> act){
        if (act == null) return;
        m_onCheckedEvent.AddListener(act);
    }
    public void OnCheckedListenerUnRegister(UnityAction<CheckPointController> act){
        if (act == null) return;
        m_onCheckedEvent.RemoveListener(act);
    }
}
