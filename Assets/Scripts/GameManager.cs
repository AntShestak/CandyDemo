using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        Time.timeScale = 1;
    }

    //method that handles Games Over behaviour
    public void GameOver()
    {
        Invoke("GameOverDelay", 2.5f);
        //Time.timeScale = 0;
        
    }

    private void GameOverDelay()
    {
        SetOnPause(true);
        ScoreManager.instance.GameOver();    
    }

    public void SetOnPause(bool value)
    {
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0); //Loads main menu
    }
}
