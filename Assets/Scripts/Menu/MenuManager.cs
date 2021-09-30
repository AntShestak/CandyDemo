using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject m_mainPanel;
    public GameObject m_highscorePanel;
    public GameObject[] m_highscoreRows;

   
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FromHighscoreToMain()
    {
        m_highscorePanel.SetActive(false);
        m_mainPanel.SetActive(true);
    }

    public void ToHighscorePanel()
    {
        //prepare highscore panel
        List<Highscore> scores = SaveManager.instance.LoadHighscores();

        m_mainPanel.SetActive(false);
        m_highscorePanel.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            if (i < scores.Count)
            {
                Text[] texts = m_highscoreRows[i].GetComponentsInChildren<Text>();
                texts[1].text = scores[i].name;
                texts[2].text = scores[i].score.ToString();
                //activate rows
                m_highscoreRows[i].SetActive(true);
            }
        }

    }
}
