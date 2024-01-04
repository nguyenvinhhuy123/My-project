using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class CheckPointController : MonoBehaviour
{   
    private readonly string PLAYER_TAG = "Player";
    private readonly string ANIMATOR_CHECKED_PARAM = "Checked";
    private Collider2D m_collider2D;
    private Animator m_animator;
    public Vector3 Position {get {return transform.position;}}
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_collider2D = GetComponent<Collider2D>();
        m_collider2D.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            OnChecked();
        }
    }
    void OnChecked()
    {
        m_animator.SetBool(ANIMATOR_CHECKED_PARAM, true);
        //*Disable collider2D on Checked so that we can not checked a checkpoint multiple time
        m_collider2D.enabled = false;
    }
}
