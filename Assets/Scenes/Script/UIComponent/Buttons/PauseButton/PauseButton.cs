using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : BaseButton
{
    public override void OnClick()
    {
        base.OnClick();
        if (!GameManager.Instance.IsPause) PauseGameClicked();
        else UnPauseGameClicked();
    }
    public void Press()
    {   
        Debug.Log("Pressed");
    }
    void PauseGameClicked(){
        GameManager.Instance.PauseGame();
        PauseMenu.Instance.OpenPanel();
    }
    void UnPauseGameClicked()
    {
        GameManager.Instance.UnPauseGame();
        PauseMenu.Instance.ClosePanel();
    }
}
