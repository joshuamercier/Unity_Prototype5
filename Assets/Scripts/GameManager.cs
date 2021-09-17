using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Class variables
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI difficultyText;
    public Button restartButton;
    public bool isGameActive = true;
    public GameObject pauseScreen;

    public int lives = 3;
    public bool isGamePaused;

    private int score = 0;
    private float spawnRate = 1.0f;
    private MainManager mainManager;
    private AudioSource backgroundMusic;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePause();
        }
    }
    private void Awake()
    {
        Debug.Log("Awake called");
        mainManager = FindObjectOfType<MainManager>();
        backgroundMusic = FindObjectOfType<AudioSource>();
        UpdateLives();
        StartGame(mainManager.gameDifficulty);
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    
    public void UpdateDifficultyText()
    {
        string difficultyInput;
        int diff = mainManager.gameDifficulty;

        if (diff == 1)
        {
            difficultyInput = "Easy";
        }
        else if(diff == 2)
        {
            difficultyInput = "Medium";
        }
        else
        {
            difficultyInput = "Hard";
        }

        difficultyText.text = difficultyInput;
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        UpdateHighScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateHighScore()
    {
        if(score > mainManager.highScore)
        {
            mainManager.highScore = score;
        }
        mainManager.SaveHighScore();
    }

    public void StartGame(int difficulty)
    {
        // Set background music volume
        backgroundMusic.volume = mainManager.musicVolume;
        // Set game difficulty and update the difficulty UI
        spawnRate /= difficulty;
        UpdateDifficultyText();
        // Start spawning of the objects
         StartCoroutine(SpawnTarget());    

        // Update score UI
        UpdateScore(score);
    }

    private void ChangePause()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isGamePaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    } 
}
