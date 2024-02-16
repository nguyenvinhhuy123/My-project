using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : PersistenceSingleton<GameManager>
{
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
        Time.timeScale = 0f;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }
}
