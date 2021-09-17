using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int gameDifficulty;    // Difficulty of the game chosen by the player
    public int highScore = 0;     // High score of the player
    public float musicVolume;     // Volume of the music set by the player

    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        AdjustVolume();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();

    }
    public void AdjustVolume()
    {
        volumeSlider = FindObjectOfType<Slider>();
        musicVolume = volumeSlider.value;
    }

    [System.Serializable]
    class SaveData
    {
        public int PlayerHighScore;
    }

    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.PlayerHighScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.PlayerHighScore;
        }
    }
}
