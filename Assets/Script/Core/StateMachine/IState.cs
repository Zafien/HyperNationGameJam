using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public interface IState
{
    void OnEnter();
    void Update();
    void FixedUpdate();
    void OnExit();
}
