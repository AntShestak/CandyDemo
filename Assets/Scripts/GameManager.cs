using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private CanvasController m_canvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        m_canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method that handles Games Over behaviour
    public void GameOver()
    {
        m_canvas.GameOver();
        Time.timeScale = 0;
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
