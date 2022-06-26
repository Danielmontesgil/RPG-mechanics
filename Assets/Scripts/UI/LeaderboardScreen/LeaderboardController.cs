using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour, IUIController
{
    [SerializeField] private LeaderboardEntry _entryPrefab;
    [SerializeField] private GameObject _scrollContent;

    public void Init()
    {
        gameObject.SetActive(true);

        PlayFabManager.Instance.GetLeaderboard(Env.HIGH_SCORE_LEADERBOARD,
            OnRequestSucceeded:
            (leaderboard) =>
            {
                if(leaderboard.Count == 0)
                {
                    Debug.Log("No players in this leaderboard");
                    return;
                }

                foreach(var player in leaderboard)
                {
                    var entry = Instantiate(_entryPrefab, _scrollContent.transform);
                    entry.Init(player.Position, player.DisplayName, player.StatValue);
                }
            });
    }

    public void Disable()
    {
        foreach(Transform child in _scrollContent.transform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
    }
}
