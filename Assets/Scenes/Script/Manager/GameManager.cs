using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : PersistenceSingleton<GameManager>
{
    private MainCharacterController m_character;
    [SerializeField] private int m_characterLife;
    public int CharacterLife {get {return m_characterLife;}}
    [SerializeField] private int m_reSpawnWaitTime; 
    public int ReSpawnWaitTime {get {return m_reSpawnWaitTime;}}
    protected override void Awake() {
        base.Awake();

        m_character = FindAnyObjectByType<MainCharacterController>()
        .GetComponent<MainCharacterController>();

    }
}
