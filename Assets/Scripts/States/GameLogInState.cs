using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogInState : GameBaseState
{
    public override void OnEnterState()
    {
        UIManager.Instance.SetupUIController(Env.LOG_IN_CONTROLLER);
    }

    public override void OnExitState()
    {
        UIManager.Instance.DisableUIController(Env.LOG_IN_CONTROLLER);
    }
}
