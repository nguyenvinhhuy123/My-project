using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIconUI : MonoBehaviour
{
    private Animator m_animator;
    private readonly string APPEAR_ANIM = "Appear";
    private readonly string DISAPPEAR_ANIM = "Disappear";
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    void Start()
    {
        m_animator.Play(APPEAR_ANIM);
    }
    /// <summary>
    /// method to remove this LifeIcon on UI canva
    /// </summary>
    public void RemoveSelf()
    {
        m_animator.Play(DISAPPEAR_ANIM);
    }
    private void OnDisappearAnimationEnd()
    {
        Destroy(this.gameObject);
    }

}
