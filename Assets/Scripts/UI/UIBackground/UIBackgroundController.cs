using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackgroundController : MonoBehaviour, IUIController
{    
    public void Init()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
