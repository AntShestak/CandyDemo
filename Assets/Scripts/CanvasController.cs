using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject m_gameOverPanel;
    public Text m_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //method to update score text
    public void UpdateScore(int score)
    {
        //update score text
        m_scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        //set Game over text
        m_gameOverPanel.SetActive(true);
    }

 
}
