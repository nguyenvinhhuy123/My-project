using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerLifeCounter : MonoBehaviour
{
    [SerializeField] private int m_characterLife;
    public int CharacterLife {get {return m_characterLife;}}
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
    public void OnCharacterDestroy()
    {
        m_lifeCounter--;
        if (m_lifeCounter == 0)
        {
            OnNoMoreLife();
        }
    }
    public void ResetCounter()
    {
        m_lifeCounter = m_characterLife;
    }
    public void OnNoMoreLife()
    {
        //TODO: Call OnGameOver Event
        Debug.Log("Out of Life");
        return;
    }
}
