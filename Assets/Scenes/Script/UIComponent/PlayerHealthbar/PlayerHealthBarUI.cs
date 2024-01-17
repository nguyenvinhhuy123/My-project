using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;
using System;

public class PlayerHealthBarUI : Singleton<PlayerHealthBarUI>
{
    private Slider m_healthBarUI;
    private int m_cacheCurrentHealthValue;
    [SerializeField][Range(1,5)] private int m_sliderChangeLerpAmount = 2;
    [SerializeField] private float m_sliderChangeLerpTimeSeconds = 0.1f;
    protected override void Awake()
    {
        base.Awake();
        m_healthBarUI = gameObject.GetComponent<Slider>();
    }
    void Start()
    {
        ResetHealthValue();
    }
    public void SetMaxHealthValue(int maxHealth)
    {
        m_healthBarUI.maxValue = maxHealth;
        //*Set minValue to 0f to clamp healthBar*/
        m_healthBarUI.minValue = 0f;
    }
    public void AdjustHealthValue(int newValue)
    {
        StartCoroutine(nameof(SliderHealthChangeLerpDown),newValue);
    }
    public void ResetHealthValue()
    {
        m_healthBarUI.value = m_healthBarUI.maxValue;
        m_cacheCurrentHealthValue = (int)m_healthBarUI.maxValue;
    }
    private IEnumerator SliderHealthChangeLerpDown(int targetValue)
    {
        int currentValue = m_cacheCurrentHealthValue;
        while (currentValue > targetValue)
        {
            currentValue -= m_sliderChangeLerpAmount;
            if (currentValue <= targetValue) currentValue = targetValue;
            m_healthBarUI.value = currentValue;
            Debug.Log(currentValue);
            yield return new WaitForSeconds(m_sliderChangeLerpTimeSeconds);
        }
        if (currentValue <= targetValue) 
        {
            m_cacheCurrentHealthValue = targetValue;
            yield break;
        }
        
    }
}
