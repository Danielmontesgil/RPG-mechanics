using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    public GameObject GetGameObject(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}
