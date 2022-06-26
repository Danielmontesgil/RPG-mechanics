using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardEntry : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rankText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Init(int rank, string name, int score)
    {
        _rankText.SetText(rank.ToString());
        _nameText.SetText(name);
        _scoreText.SetText(score.ToString());
    }
}
