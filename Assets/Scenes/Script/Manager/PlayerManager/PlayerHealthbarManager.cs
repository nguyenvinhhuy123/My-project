using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthbarManager : MonoBehaviour
{
    private Damageable m_cachePlayerDamageable;
    //TODO: Health UI ref here
    private UnityAction<int, bool> m_onDamagedAction;
    private UnityAction<int> m_onHealAction;
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
        m_onHealAction += OnHeal;
    }
    void OnDisable()
    {
        m_onDamagedAction -= OnDamage;
        m_onHealAction -= OnHeal;
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
    private void OnHeal(int currentHealth)
    {

    }
    private void OnCharacterSpawn(MainCharacterController character)
    {
        character.gameObject.TryGetComponent<Damageable>(out m_cachePlayerDamageable);
        m_cachePlayerDamageable.DamagedEventListenerRegister(m_onDamagedAction);
        m_cachePlayerDamageable.HealEventListenerRegister(m_onHealAction);
    }
    private void OnCharacterDestroy()
    {

    }
}
