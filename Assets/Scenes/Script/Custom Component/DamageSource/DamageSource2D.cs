using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageSource2D : MonoBehaviour
{
    [SerializeField] private int m_Damage;
    public int Damage {get {return m_Damage;} set {m_Damage = value;}}

    private GameObject m_attachedGO;

    private UnityEvent<int> dealDamageEvent;
    // Start is called before the first frame update
    void Awake() 
    {
        m_attachedGO = gameObject;
        dealDamageEvent = new UnityEvent<int>();
    }
    void Start()
    {
        m_attachedGO = gameObject;
    }
    /// <summary>
    /// register UnityAction for dealDamageEvent
    /// Event Invoke will return int damage value
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void EventListenerRegister(UnityAction<int> act)
    {
        dealDamageEvent.AddListener(act);
    }
    /// <summary>
    /// unregister UnityAction for dealDamageEvent
    /// Event Invoke will return int damage value
    /// </summary>
    /// <param name="act"></param>Listener Action need to register<summary>
    public void EventListenerUnregister(UnityAction<int> act)
    {
        dealDamageEvent.RemoveListener(act);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: Collision mechanism connected to Damagaeble
        if (other.gameObject.TryGetComponent<Damageable>(out Damageable damageable))
        {
            damageable.OnDamaged(m_Damage, gameObject.GetComponent<DamageSource2D>());
        }
    }
    //TODO: ACtion register
}
