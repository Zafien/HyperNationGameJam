using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : GameBaseState
{
    public PlayingState(GameState gameState) : base(gameState)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log($"Play State");
    }
}
