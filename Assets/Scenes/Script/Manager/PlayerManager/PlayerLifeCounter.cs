using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerLifeCounter : MonoBehaviour
{
    [SerializeField] private int m_characterMaxLife;
    public int CharacterMaxLife {get {return m_characterMaxLife;}}
    private int m_lifeCounter;
    // Start is called before the first frame update
    void Start()
    {
        ResetCounter();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCharacterDestroy()
    {
        m_lifeCounter--;
        if (m_lifeCounter == 0)
        {
            OnNoMoreLife();
        }
    }
    public void ResetCounter()
    {
        m_lifeCounter = m_characterMaxLife;
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
        if (m_lifeCounter >= CharacterMaxLife) 
        {
            m_lifeCounter = CharacterMaxLife;
            return;
        }
    }
}
