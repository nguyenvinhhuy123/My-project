using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter();
    void OnExit();
    void OnInputHandle();
    void OnUpdate();
    void OnFixedUpdate();
}
