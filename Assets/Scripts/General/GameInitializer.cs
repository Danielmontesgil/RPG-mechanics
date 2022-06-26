using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Init();
        ResourcesManager.Init();
        PoolManager.Init();
        EventManager.Init();
        PlayFabManager.Init();
    }
}
