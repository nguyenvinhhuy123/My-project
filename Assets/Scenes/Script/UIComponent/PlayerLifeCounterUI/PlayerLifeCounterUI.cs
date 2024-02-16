using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;

public class PlayerLifeCounterUI : Singleton<PlayerLifeCounterUI>
{
    public static readonly int MAX_RENDER_LIFE_ICON = 10;
    private LifeIconUI[] m_LifeIconList = new LifeIconUI[MAX_RENDER_LIFE_ICON]; 
    private int m_currentIndex = 0;
    [SerializeField] private GameObject m_LifeIconPrefab;
    [SerializeField] private int m_iconDistance;
    
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddLifeIcon();
        }
    }
    /// <summary>
    /// Add a Life Icon
    /// Cap at MAX_RENDER_LIFE_ICON
    /// </summary>
    public void AddLifeIcon()
    {
        if (m_currentIndex >= m_LifeIconList.Length)
        {
            m_currentIndex = m_LifeIconList.Length;
            return;
        }
        Vector3 target_position = new Vector3(m_currentIndex*m_iconDistance,0,0);
        m_LifeIconList[m_currentIndex] = Instantiate(m_LifeIconPrefab,this.transform, false)
                                        .GetComponent<LifeIconUI>();
        m_LifeIconList[m_currentIndex].transform.localPosition = target_position;
        Debug.Log(this.transform);
        m_currentIndex++;
    }
    /// <summary>
    /// Remove a Life Icon
    /// </summary>
    public void RemoveLifeIcon()
    {
        if (m_currentIndex <= 0)
        {
            m_currentIndex = 0;
            return;
        }
        m_currentIndex--;
        //**/
        m_LifeIconList[m_currentIndex].RemoveSelf();
        m_LifeIconList[m_currentIndex] = null;
        
    }
    /// <summary>
    /// Set the number of icons to number of life currently have
    /// </summary>
    /// <param name="lifeToSet">Number of life currently have (get from PlayerLifeCounter)</param>
    public void SetLifeCounter(int lifeToSet = 1)
    {
        while (m_currentIndex < lifeToSet)
        {
            Debug.Log("set life counter");
            if (m_currentIndex >= m_LifeIconList.Length - 1) break;
            AddLifeIcon();
        }
    }
}
