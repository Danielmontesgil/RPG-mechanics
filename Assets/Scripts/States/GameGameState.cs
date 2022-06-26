using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGameState : GameBaseState
{
    public override void OnEnterState()
    {
        UIManager.Instance.DisableUIController(Env.UI_BACKGROUND_CONTROLLER);
        UIManager.Instance.SetupUIController(Env.GAME_UI_CONTROLLER);
    }

    public override void OnExitState()
    {
        UIManager.Instance.SetupUIController(Env.UI_BACKGROUND_CONTROLLER);
        UIManager.Instance.DisableUIController(Env.GAME_UI_CONTROLLER);
    }
}
