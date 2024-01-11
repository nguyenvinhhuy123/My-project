using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    //*Max health of this obj*/
    [SerializeField] private int m_Health;
    public int Health {get {return m_Health;} set {m_Health = value;}}
    //*Track Current health of this obj*/
    [SerializeField] private int m_CurrentHealth;
    private GameObject m_attachedGO;
    [SerializeField] private float m_IFrameTime;
    public float IFrameTime {get {return m_IFrameTime;} set {m_IFrameTime = value;}}
    private float m_IFrameTimer;
    /*
    *m_damageEvent<currentHealth: int, isDead_:bool>
    */
    private UnityEvent<int, bool> m_onDamagedEvent;

    /*
    *m_healEvent<currentHealth: int>
    */
    private UnityEvent<int> m_onHealEvent;
    void Awake()
    {
        m_attachedGO = gameObject;
        m_onDamagedEvent = new UnityEvent<int, bool>();
        ResetHealth();
    }
    void Update()
    {
        //Update timer call for IFrameTimer
        m_IFrameTimer -= Time.deltaTime; 
    }
    void OnDisable()
    {
    }
    /// <summary>
    /// Reset this obj current health to max_health
    /// </summary>
    public void ResetHealth()
    {
        m_CurrentHealth = m_Health;
    } 
    /// <summary>
    /// Method to call when damage this damageable from outer source
    /// </summary>
    /// <param name="inputDamage">taken damage</param>
    /// <param name="source">dmg source</param>
    public void Damaged(int inputDamage, DamageSource2D source)
    {
        bool isDead = false;
        if (m_IFrameTimer >= 0f)
        {
            return;
        } 
        m_CurrentHealth -= inputDamage;
        if (m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            isDead =true;
        }
        m_onDamagedEvent.Invoke(m_CurrentHealth, isDead);
        if (isDead) return;
        m_IFrameTimer = m_IFrameTime;
        StartCoroutine(IFrame(source.gameObject.layer));
    }
    /// <summary>
    /// Method to call when heal this damageable from outer source
    /// </summary>
    /// <param name="healAmount"> amount of input healing</param>
    public void Heal(int healAmount)
    {
        m_CurrentHealth += healAmount;
        if (m_CurrentHealth >= m_Health)
        {
            ResetHealth();
        }
        m_onHealEvent.Invoke(m_CurrentHealth);

    }
    /// <summary>
    /// IFrame coroutine callback
    /// </summary>
    /// <param name="otherLayer">layer to ignore collision, 
    /// should be the layer of the object that cause last dmg</param>
    /// <returns></returns>
    private IEnumerator IFrame(int otherLayer)
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, otherLayer, true);
        yield return new WaitUntil(() => m_IFrameTimer <= 0f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, otherLayer, false);
    }
    /// <summary>
    /// register UnityAction for damagedEvent
    /// Event Invoke will return int currentHealth and isDead bool
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void DamagedEventListenerRegister(UnityAction<int, bool> act)
    {
        if (act == null) return;
        m_onDamagedEvent.AddListener(act);
    }
    /// <summary>
    /// unregister UnityAction for damagedEvent
    /// Event Invoke will return int currentHealth and isDead bool
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void DamagedEventListenerUnregister(UnityAction<int, bool> act)
    {
        if (act == null) return;
        m_onDamagedEvent.RemoveListener(act);
    }
    /// <summary>
    /// register UnityAction for onHealEvent
    /// Event Invoke will return int currentHealth
    /// </summary>
    /// <param name="act">action to register</param>
    public void HealEventListenerRegister(UnityAction<int> act)
    {
        if (act == null) return;
        m_onHealEvent.AddListener(act);
    }
    /// <summary>
    /// Unregister UnityAction for onHealEvent
    /// Event Invoke will return int currentHealth
    /// </summary>
    /// <param name="act"></param>action to cancel register<summary>
    public void HealEventListenerUnRegister(UnityAction<int> act)
    {
        if (act == null) return;
        m_onHealEvent.RemoveListener(act);
    }
}
