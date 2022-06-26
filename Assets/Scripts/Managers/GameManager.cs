using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameBaseState _currentGameState;
    private GameBaseState _previousGameState;

    public GameBaseState CurrentGameState => _currentGameState;
    public GameBaseState PreviousGameState => _previousGameState;

    void Start()
    {
        if (EditorHacksManager.Instance._skipGameMenu)
        {
            ChangeGameState(new GameGameState());
            return;
        }
        ChangeGameState(new GameLogInState());
    }

    void Update()
    {
        _currentGameState.OnUpdateState();
    }

    public void ChangeGameState(GameBaseState newGameState)
    {
        _previousGameState = _currentGameState;
        _currentGameState = newGameState;

        _previousGameState?.OnExitState();
        _currentGameState?.OnEnterState();
    }
}
