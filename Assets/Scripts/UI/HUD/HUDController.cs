using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, IUIController
{
    [SerializeField] private Button _backButton;

    public void Init()
    {
        gameObject.SetActive(true);

        SetupButtons();
    }

    public void Disable()
    {
        ResetButtons();

        gameObject.SetActive(false);
    }

    private void SetupButtons()
    {
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void ResetButtons()
    {
        _backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClick()
    {
        GameManager.Instance.ChangeGameState(GameManager.Instance.PreviousGameState);
    }
}
