using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.UI;

public class PlayerLifeCounterUI : Singleton<PlayerLifeCounterUI>
{
    private static readonly int MAX_RENDER_LIFE_ICON = 10;
    private Image[] m_LifeIconList = new Image[MAX_RENDER_LIFE_ICON]; 
    [SerializeField] private GameObject m_LifeIconPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnLifeAdded()
    {

    }
    private void RenderIconList()
    {
        
    }
}
