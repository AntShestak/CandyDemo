using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    CanvasController m_canvas;
    bool m_isGameOver = false;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        m_canvas = FindObjectOfType<CanvasController>();

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
        m_isGameOver = true;
        SetPauseOn();
        m_canvas.GameOver();    
    }

    public void SetPauseOn() => Time.timeScale = 0;
    public void SetPauseOff() => Time.timeScale = 1.0f;
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0); //Loads main menu
    }
}
