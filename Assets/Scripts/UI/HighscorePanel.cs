using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscorePanel : MonoBehaviour
{
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_mainMenuButton;

    [SerializeField] GameObject[] m_highscoreRows;

    private void OnEnable()
    {
        m_restartButton.onClick.AddListener(GameManager.instance.RestartGame);
        m_mainMenuButton.onClick.AddListener(GameManager.instance.GoToMenu);


    }

    public void DisplayHighscores(List<Highscore> highscore, int playerPosition)
    {
        for (int i = 0; i < highscore.Count; i++)
        {
            if (i >= 4)
            {
                Debug.Log("Error. Too many highscores");
            }
            else
            {
                Text[] texts = m_highscoreRows[i].GetComponentsInChildren<Text>();
                //if player position is more or equal to fifth have to adjust enumeration on last line
                if (i == 3 && playerPosition >= 5)
                    texts[0].text = playerPosition.ToString();
                texts[1].text = highscore[i].name;
                texts[2].text = highscore[i].score.ToString();
                    
            }
        }

        FindObjectOfType<CanvasController>().ShowHighscoresPanel();
    }
}
