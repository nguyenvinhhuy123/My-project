using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int m_Health;
    public int Health {get {return m_Health;} set {m_Health = value;}}
    private int m_CurrentHealth;
    private GameObject m_attachedGO;

    [SerializeField]private float m_IFrameTime;
    public float IFrameTime {get {return m_IFrameTime;} set {m_IFrameTime = value;}}

    private float m_IFrameTimer;

    private UnityEvent<int, bool> m_damagedEvent;
    void Awake()
    {
        m_attachedGO = gameObject;
        m_damagedEvent = new UnityEvent<int, bool>();
        onResetHealth();
    }
    void Update()
    {
        m_IFrameTimer -= Time.deltaTime;   
    }

    public void onResetHealth()
    {
        m_CurrentHealth = m_Health;
    } 
    public void onDamaged(int InputDamage)
    {
        bool isDead = false;
        if (m_IFrameTimer >= 0f)
        {
            
            return;
        } 
        m_CurrentHealth -= InputDamage;
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            isDead =true;
        }
        m_damagedEvent.Invoke(m_CurrentHealth, isDead);
        m_IFrameTimer = m_IFrameTime;
    }
    /// <summary>
    /// register UnityAction for damagedEvent
    /// Event Invoke will return int currentHealth and isDead bool
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void EventListenerRegister(UnityAction<int, bool> act)
    {
        m_damagedEvent.AddListener(act);
    }
    /// <summary>
    /// unregister UnityAction for damagedEvent
    /// Event Invoke will return int currentHealth and isDead bool
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void EventListenerUnregister(UnityAction<int, bool> act)
    {
        m_damagedEvent.RemoveListener(act);
    }
}
