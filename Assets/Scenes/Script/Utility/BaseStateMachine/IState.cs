using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
public interface IState
{
    void OnEnter();
    void OnExit();
    void OnInputHandle();
    void OnUpdate();
    void OnFixedUpdate();
}
}