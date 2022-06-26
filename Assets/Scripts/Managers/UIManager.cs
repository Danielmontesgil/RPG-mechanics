using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("HUD")]
    [SerializeField] private HUDController _hudController;

    [Header("Login")]
    [SerializeField] private LogInController _logInController;

    [Header("Menu")]
    [SerializeField] private MenuController _menuController;

    [Header("Game")]
    [SerializeField] private GameUIController _gameUIController;

    [Header("Leaderboard")]
    [SerializeField] private LeaderboardController _leaderboardController;

    [Header("Settings")]
    [SerializeField] private SettingsController _settingsController;

    [Header("Background")]
    [SerializeField] private UIBackgroundController _backgroundController;

    private Dictionary<string, IUIController> _controllers = new Dictionary<string, IUIController>();

    private void Awake()
    {
        Init(this);

        _controllers.Add(Env.HUD_CONTROLLER, _hudController);
        _controllers.Add(Env.LOG_IN_CONTROLLER, _logInController);
        _controllers.Add(Env.MENU_CONTROLLER, _menuController);
        _controllers.Add(Env.GAME_UI_CONTROLLER, _gameUIController);
        _controllers.Add(Env.LEADERBOARD_CONTROLLER, _leaderboardController);
        _controllers.Add(Env.SETTINGS_CONTROLLER, _settingsController);
        _controllers.Add(Env.UI_BACKGROUND_CONTROLLER, _backgroundController);
    }

    public void SetupUIController(string controllerKey)
    {
        var controller = FindController(controllerKey);
        controller?.Init();
    }

    public void DisableUIController(string controllerKey)
    {
        var controller = FindController(controllerKey);
        controller?.Disable();
    }

    private IUIController FindController(string controllerKey)
    {
        return _controllers[controllerKey];
    }
}
