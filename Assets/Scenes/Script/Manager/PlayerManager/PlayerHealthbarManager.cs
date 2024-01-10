using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthbarManager : MonoBehaviour
{
    private Damageable m_cachePlayerDamageable;
    //TODO: Health UI ref here
    private UnityAction m_onCharacterDestroyAction;
    private UnityAction<MainCharacterController> m_onCharacterSpawnAction;
    private UnityAction<int, bool> m_onDamagedAction;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        
    }
    void OnEnable()
    {
        m_onDamagedAction += OnDamage;
        m_onCharacterSpawnAction += OnCharacterSpawn;
        m_onCharacterDestroyAction += OnCharacterDestroy;
    }
    void OnDisable()
    {
        m_onDamagedAction -= OnDamage;
        m_onCharacterSpawnAction -= OnCharacterSpawn;
        m_onCharacterDestroyAction -= OnCharacterDestroy;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDamage(int currentHealth, bool isDead)
    {
        //TODO: call healthbar change
        if (isDead)
        {

        }

    }
    private void OnCharacterSpawn(MainCharacterController character)
    {
        character.gameObject.TryGetComponent<Damageable>(out m_cachePlayerDamageable);
    }
    private void OnCharacterDestroy()
    {
        m_cachePlayerDamageable = null;
    }
}
