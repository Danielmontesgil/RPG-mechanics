using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuState : GameBaseState
{
    public override void OnEnterState()
    {
        UIManager.Instance.SetupUIController(Env.MENU_CONTROLLER);
    }

    public override void OnExitState()
    {
        UIManager.Instance.DisableUIController(Env.MENU_CONTROLLER);
    }
}
