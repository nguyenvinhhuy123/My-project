using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour, IButton
{
    protected Button m_Btn;
    protected Animator m_animator;
    #region button animation
    string ON_CLICK = "OnClick";
    string ON_HOVER = "OnHover";
    string ON_EXIT_HOVER = "OnExitHover";
    #endregion
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_Btn = GetComponent<Button>();
    }
    public virtual void OnClick()
    {
        
    }
    public virtual void OnHover()
    {

    }
    public virtual void OnExitHover()
    {
        
    }
}
