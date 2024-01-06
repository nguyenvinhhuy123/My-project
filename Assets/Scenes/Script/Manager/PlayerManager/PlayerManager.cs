using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlayerManager : PersistenceSingleton<PlayerManager>
{
    private PlayerSpawner _spawner;
    private PlayerLifeCounter _lifeCounter;
    private MainCharacterController m_characterInstance;
    public MainCharacterController CharacterInstance {get{ return m_characterInstance;}}
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
        gameObject.BroadcastMessage("OnCharacterDestroy", SendMessageOptions.DontRequireReceiver);
    }
    public void CharacterSpawn(MainCharacterController character)
    {  
        gameObject.BroadcastMessage("OnCharacterSpawn", character, SendMessageOptions.DontRequireReceiver);
    }
    public void Checked(CheckPointController checkPoint)
    {
        gameObject.BroadcastMessage("OnChecked", checkPoint, SendMessageOptions.DontRequireReceiver);
    }
    private void OnCharacterSpawn(MainCharacterController character)
    {
        m_characterInstance = character;
    }
}
