using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameBaseState
{
    public PauseState(GameState gameState) : base(gameState)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log($"Enter Pause State");
        Time.timeScale = 0f;
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log($"Exit Pause State");
        Time.timeScale = 1f;
    }
}
