using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : BaseButton
{
    public override void OnClick()
    {
        base.OnClick();
        if (!GameManager.Instance.IsPause) GameManager.Instance.PauseGame();
        else  GameManager.Instance.UnPauseGame();
    }
    public void Press()
    {   
        Debug.Log("Pressed");
    }
}
