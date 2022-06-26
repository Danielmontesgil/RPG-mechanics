using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IUIController
{
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _settingButton;

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
        _playGameButton.onClick.AddListener(OnPlayGameClick);
        _leaderboardButton.onClick.AddListener(OnLeaderboardClick);
        _settingButton.onClick.AddListener(OnSettingsClick);
    }

    private void ResetButtons()
    {
        _playGameButton.onClick.RemoveAllListeners();
        _leaderboardButton.onClick.RemoveAllListeners();
        _settingButton.onClick.RemoveAllListeners();
    }

    private void OnPlayGameClick()
    {
        GameManager.Instance.ChangeGameState(new GameGameState());
    }

    private void OnLeaderboardClick()
    {
        GameManager.Instance.ChangeGameState(new GameLeaderboardState());
    }

    private void OnSettingsClick()
    {
        GameManager.Instance.ChangeGameState(new GameSettingsState());
    }
}
