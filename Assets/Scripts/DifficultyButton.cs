using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // Class variables
    public int difficulty;

    [SerializeField] Button button;
    [SerializeField] MainManager mainManager;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SetDifficulty);
        mainManager = FindObjectOfType<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDifficulty()
    {
        Debug.Log( button.gameObject.name + " was clicked");
        mainManager.gameDifficulty = difficulty;
        mainManager.StartNewGame();
    }
}
