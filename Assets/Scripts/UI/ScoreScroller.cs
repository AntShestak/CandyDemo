using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScroller : MonoBehaviour
{
    public RowScript[] m_rows;

    private void OnEnable()
    {
        //since the object can be siabled in midst of scroll process
        //prcess has to be resumed or resterted on enabling
        ScoreManager scoreMan = ScoreManager.instance;
        if (scoreMan != null && scoreMan.Score != 0)
            InstantScroll(scoreMan.Score);
    }

    private void InstantScroll(int score)
    {
        //rather than using coroutine, instantly put's the right score on display
        //get score length
        int l = IntLength(score);
        int rowLen = m_rows.Length;

        int div = 10; //divisor

        //break the score into digits
        for (int i = rowLen - 1; i >= rowLen - l; i--)
        {

            //set row to correct number
            int num = score % div / (div / 10);
            m_rows[i].SetNumberInstant(num);
            //Debug.Log("Line " + i.ToString() + " set to: " + num.ToString());
            //adjust the score
            score -= num;
            //increase divisor
            div *= 10;
        }
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

        IEnumerator cor = SetScoreCoroutine(score);
        StartCoroutine(cor);

    }

    IEnumerator SetScoreCoroutine(int score)
    {
        //get score length
        int l = IntLength(score);
        int rowLen = m_rows.Length;

        int div = 10; //divisor

        //cache wait for seconds object
        var waitForSeconds = new WaitForSeconds(0.1f);

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
            yield return waitForSeconds;
        }
    }

    //method returns length of integer
    private int IntLength(int i)
    {
        if (i == 0)
            return 1;
        return (int)Mathf.Floor(Mathf.Log10(i)) + 1;
    }
}
