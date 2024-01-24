using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerLifeCounter : MonoBehaviour
{
    private static readonly int m_characterMaxLife = PlayerLifeCounterUI.MAX_RENDER_LIFE_ICON;
    public int CharacterMaxLife {get {return m_characterMaxLife;}}
    [SerializeField] private int m_characterStartLife;
    public int CharacterStartLife {get {return m_characterStartLife;}}
    private int m_lifeCounter;
    // Start is called before the first frame update
    void Start()
    {
        //* Just give this max render value is max number life for ease of life xD
        ResetCounter();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCharacterDestroy()
    {
        m_lifeCounter--;
        PlayerLifeCounterUI.Instance.RemoveLifeIcon();
        if (m_lifeCounter == 0)
        {
            OnNoMoreLife();
        }
    }
    public void ResetCounter()
    {
        m_lifeCounter = m_characterStartLife;
        PlayerLifeCounterUI.Instance.SetLifeCounter(m_lifeCounter);
    }
    private void OnNoMoreLife()
    {
        //TODO: Call OnGameOver Event
        Debug.Log("Out of Life");
        return;
    }
    public void AddLife()
    {
        m_lifeCounter++; 
        PlayerLifeCounterUI.Instance.AddLifeIcon();
        if (m_lifeCounter >= CharacterMaxLife) 
        {
            m_lifeCounter = CharacterMaxLife;
            return;
        }
    }
}
