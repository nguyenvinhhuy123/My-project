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

    void OnCollisionEnter2D(Collision2D other)
    {
        //TODO: Collision mechanism connected to Damagaeble
    }
    //TODO: ACtion register
}
