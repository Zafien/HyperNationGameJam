using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class GameBaseState : IState
{
    protected readonly GameState _gameState;

    protected const float _crossFadeDuration = 0.1f;

    protected GameBaseState(GameState gameState) => _gameState = gameState;


    public virtual void OnEnter() { }
    public virtual void FixedUpdate() { }
    public virtual void Update() { }    
    public virtual void OnExit() { }
}

public enum GameState
{
    Paused,
    Playing,
    GameOver,
    GameWon,
    GameStart,
    GameDialogue,
    FTUE,
}

