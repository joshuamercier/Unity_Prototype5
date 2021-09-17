using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    private MainManager mainManager;

    void Awake()
    {
        // Get the text and main manager
        highScoreText = GetComponentInChildren<TextMeshProUGUI>();
        mainManager = FindObjectOfType<MainManager>();
        // Update the high score text
        mainManager.LoadHighScore();
        highScoreText.text = "High Score: " + mainManager.highScore;
    }
}
