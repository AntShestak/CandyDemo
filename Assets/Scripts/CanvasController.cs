using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject m_gameOverPanel;
    public Text m_scoreText;

    public RowScript[] m_rows;

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

    public void GameOver()
    {
        //set Game over text
        m_gameOverPanel.SetActive(true);
    }

    //method returns length of integer
    private int IntLength(int i)
    {
        if (i == 0)
            return 1;
        return (int)Mathf.Floor(Mathf.Log10(i)) + 1;
    }


}
