using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IButton : ISelectHandler
{
    void OnClick();
    void OnHover();
    void OnExitHover();
}
