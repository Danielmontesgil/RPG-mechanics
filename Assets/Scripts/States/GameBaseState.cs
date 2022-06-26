using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void OnEnterState();

    public abstract void OnExitState();

    public virtual void OnUpdateState(){ }
}
