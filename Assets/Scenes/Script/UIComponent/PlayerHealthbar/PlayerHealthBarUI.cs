using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;

public class PlayerHealthBarUI : Singleton<PlayerHealthBarUI>
{
    private Slider m_healthBarUI;
    private int m_cacheCurrentHealthValue;
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
    private IEnumerator ValueChange(int targetValue, int currentValue)
    {
        yield return null;
    }
}
