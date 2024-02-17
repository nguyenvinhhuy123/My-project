using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour, IButton
{
    protected Button m_Btn;
    protected Animator m_animator;
    #region button animation
    private readonly string ON_CLICK = "Click";
    private readonly string ON_HOVER = "Hover";
    private readonly string ON_NORMAL = "Normal";
    private readonly string ON_SELECT = "Select";
    #endregion
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_Btn = GetComponent<Button>();
        Debug.Log("Start");
    }
    public virtual void OnSelect(BaseEventData eventData)
    {
        m_animator.Play(ON_SELECT);
        Debug.Log(ON_SELECT);
    }
    public virtual void OnClick()
    {
        m_animator.Play(ON_CLICK);
        Debug.Log(ON_CLICK);
    }
    public virtual void OnHover()
    {
        m_animator.Play(ON_HOVER);
        Debug.Log(ON_HOVER);
    }
    public virtual void OnExitHover()
    {
        m_animator.Play(ON_NORMAL);
        Debug.Log(ON_NORMAL);
    }
}
