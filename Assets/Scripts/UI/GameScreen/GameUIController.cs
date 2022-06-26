using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour, IUIController
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void Init()
    {
        gameObject.SetActive(true);

        InitScoreText();
    }

    public void Disable()
    {
        InitScoreText();

        gameObject.SetActive(false);
    }

    private void InitScoreText()
    {
        _scoreText.SetText("Score: 000000");
    }
}
