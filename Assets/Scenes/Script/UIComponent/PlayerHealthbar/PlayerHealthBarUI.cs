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
    [SerializeField] private float m_sliderChangeLerpTime = 0.02f;
    protected override void Awake()
    {
        base.Awake();
        m_healthBarUI = gameObject.GetComponent<Slider>();
    }
    public void SetMaxHealthValue(int maxHealth)
    {
        m_healthBarUI.maxValue = maxHealth;
        //*Set minValue to 0f to clamp healthBar*/
        m_healthBarUI.minValue = 0f;
    }
    public void AdjustHealthValue(int newValue)
    {
        throw new System.Exception("Not Implemented");
    }
    public void ResetHealthValue()
    {
        m_healthBarUI.value = m_healthBarUI.maxValue;
    }
    private IEnumerator SliderHealthChangeLerp(int targetValue, int startValue)
    {
        int currentValue = startValue;
        if (currentValue == targetValue) yield break;

        currentValue -= m_sliderChangeLerpAmount;
        m_healthBarUI.value = currentValue;

        yield return new WaitForSeconds(m_sliderChangeLerpTime);
    }
}
