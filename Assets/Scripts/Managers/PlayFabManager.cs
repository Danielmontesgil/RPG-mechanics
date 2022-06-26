using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabManager : Singleton<PlayFabManager>
{
    public void LogInWithPlayFab(string username, string password, Action OnRequestSucceeded = null)
    {
        var request = new LoginWithPlayFabRequest { Username = username, Password = password };
        PlayFabClientAPI.LoginWithPlayFab(request, 
            (r) =>
            {
                OnRequestSucceeded?.Invoke();
            }, 
            OnRequestFailed);
    }

    public void SignUpWithPlayFab(string username, string password, string email, Action OnRequestSucceeded = null)
    {
        var request = new RegisterPlayFabUserRequest { Username = username, Password = password, Email = email, RequireBothUsernameAndEmail = true, DisplayName = username };
        PlayFabClientAPI.RegisterPlayFabUser(request,
            (r) =>
            {
                OnRequestSucceeded?.Invoke();
            }, 
            OnRequestFailed);
    }

    private void OnRequestFailed(PlayFabError error)
    {
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetLeaderboard(string leaderboardName, int startPos = 0, Action<List<PlayerLeaderboardEntry>> OnRequestSucceeded = null)
    {
        var request = new GetLeaderboardRequest { StartPosition = startPos, StatisticName = leaderboardName };
        PlayFabClientAPI.GetLeaderboard(request, 
            (r) =>
            {
                OnRequestSucceeded?.Invoke(r.Leaderboard);
            },
            OnRequestFailed);
    }

    public void UpdateStatistics(string statisticName, int value)
    {
        var statistic = new StatisticUpdate { StatisticName = statisticName, Value = value };

        var staticticsList = new List<StatisticUpdate>();
        staticticsList.Add(statistic);

        var request = new UpdatePlayerStatisticsRequest { Statistics = staticticsList };
        PlayFabClientAPI.UpdatePlayerStatistics(request,
            (r) =>
            {
                Debug.Log(statisticName + " updated");
            }, 
            OnRequestFailed);
    }
}
