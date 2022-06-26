using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLeaderboardState : GameBaseState
{
    public override void OnEnterState()
    {
        UIManager.Instance.SetupUIController(Env.LEADERBOARD_CONTROLLER);
        UIManager.Instance.SetupUIController(Env.HUD_CONTROLLER);
    }

    public override void OnExitState()
    {
        UIManager.Instance.DisableUIController(Env.LEADERBOARD_CONTROLLER);
        UIManager.Instance.DisableUIController(Env.HUD_CONTROLLER);
    }
}
