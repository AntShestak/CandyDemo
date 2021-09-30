using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject m_gameOverPanel;
    public GameObject m_enterNamePanel;
    public GameObject m_highscoresPanel;
    public GameObject[] m_highscoreRows;
    public GameObject m_scorePanel;
    public Text m_gameOverScoreText;
    [SerializeField] Button m_pauseButton;

    public RowScript[] m_rows;
    public InputField m_input;


    public Text m_debugConsole;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //method to update score text
    public void UpdateScore(int score)
    {
        //get score length
        int l = IntLength(score);

        //length should not be longer that number of rows
        if (l > m_rows.Length)
        {
            Debug.Log("Score is too long, can't proceed.");
            return;
        }

        IEnumerator cor = SetScore(score);
        StartCoroutine(cor);

        

    }

    IEnumerator SetScore(int score)
    {
        //get score length
        int l = IntLength(score);
        int rowLen = m_rows.Length;

        int div = 10; //divisor
        //break the score into digits
        for (int i = rowLen - 1; i >= rowLen - l; i--)
        {

            //set row to correct number
            int num = score % div / (div / 10);
            m_rows[i].SetNumber(num);
            //Debug.Log("Line " + i.ToString() + " set to: " + num.ToString());
            //adjust the score
            score -= num;
            //increase divisor
            div *= 10;
            //i want score row's to start spinning with delay
            yield return new WaitForSeconds(0.1f);
        }
    }

    //activate game over screen for player to enter name
    public void GameOver(int score)
    {
        
       //set Game over text
       m_gameOverPanel.SetActive(true);
       //remove score (temporary)
       m_scorePanel.SetActive(false);
       m_pauseButton.gameObject.SetActive(false);
       //enter name panel is active by DEFAULT
       m_gameOverScoreText.text = "You scored: " + score.ToString();
      
    }


    //method returns length of integer
    private int IntLength(int i)
    {
        if (i == 0)
            return 1;
        return (int)Mathf.Floor(Mathf.Log10(i)) + 1;
    }

    public void SetPlayerName()
    {
        string name = m_input.text;
        Debug.Log("Name set " + name);
        //report name to score manager so the highscores will get sorted
        ScoreManager.instance.SetPlayerName(name);
    }

    //set highscores panel
    public void ShowHighscoresPanel(List<Highscore> scores, int playerPosition)
    {
        //loop through list and activate scores
        for (int i = 0; i < scores.Count; i++)
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
                texts[1].text = scores[i].name;
                texts[2].text = scores[i].score.ToString();
                    
            }
        }
        //deactivate game over panel
        m_enterNamePanel.SetActive(false);
        //activate highscore panel
        m_highscoresPanel.SetActive(true);
    }

    public void ChangeMode(int i)
    {
        Mode mode = ModeManager.instance.CurrentMode;

        switch (i)
        {
            case 0:
                mode = Mode.Hold;
                break;
            case 1:
                mode = Mode.Tap;
                break;
            case 2:
                mode = Mode.Swipe;
                break;
            case 3:
                mode = Mode.Gyro;
                break;
            default:
                break;
        }

        ModeManager.instance.CurrentMode = mode;

    }

    public void DebguConsole(string text)
    {
        m_debugConsole.text += $"\n{text}";
    }

}
