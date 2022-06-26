using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHacksManager : Singleton<EditorHacksManager>
{
    public bool _skipGameMenu;

    private void Awake()
    {
        Init(this);
    }
}
