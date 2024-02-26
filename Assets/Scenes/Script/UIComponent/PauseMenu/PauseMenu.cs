using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PauseMenu : PersistenceSingleton<PauseMenu>
{
    private void Start() {
        ClosePanel();
    }
    public void OpenPanel()
    {
        this.gameObject.SetActive(true);
    }
    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
    }
}
