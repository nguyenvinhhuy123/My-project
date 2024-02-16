using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : PersistenceSingleton<GameManager>
{
    private bool m_isPaused = false;
    public bool IsPause {get {return m_isPaused;}}
    protected override void Awake() {
        base.Awake();
    }
    private void Start() {
    }
    private void OnGameOver()
    {

    }
    public void PauseGame()
    {
        ChangeTimeScale(0f);
        m_isPaused = true;
    }
    public void UnPauseGame()
    {
        ResetTimeScale();
        m_isPaused = false;
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void ChangeTimeScale(float percentage)
    {
        Time.timeScale = Time.timeScale*percentage*1f;
    }
}
