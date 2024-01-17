using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockHealthBarChanger : MonoBehaviour
{
    private PlayerHealthBarUI UI;
    private int m_mockHealth = 100;
    // Start is called before the first frame update
    void Awake()
    {
        UI = GetComponent<PlayerHealthBarUI>();
        UI.SetMaxHealthValue(m_mockHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_mockHealth -= 20;
            UI.AdjustHealthValue(m_mockHealth);
            if (m_mockHealth == 0)
            {
                ResetTestCondition();
            }
        }
    }
    private void ResetTestCondition()
    {
        m_mockHealth = 100;
    }
}
