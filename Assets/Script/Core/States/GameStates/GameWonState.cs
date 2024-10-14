using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonState : GameBaseState
{
    public GameWonState(GameState gameState) : base(gameState)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log($"Game Won State");
    }
}
