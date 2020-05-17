using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowScript : MonoBehaviour
{
    public float[] m_posY;
    public float m_posReset; //y position for reset
    public ParticleSystem m_ps;

    private RectTransform m_tran;
    private Vector3 m_basePos; //base position
    private int m_current = 0; //current number
    private float m_speed = 18;
    // Start is called before the first frame update
    void Start()
    {
        m_ps.Stop();
        m_tran = GetComponent<RectTransform>();
        m_basePos = m_tran.anchoredPosition;
        SetPosition(m_posY[0]);
    }

    //function to set position on Y axis
    void SetPosition(float pos)
    {
        m_tran.anchoredPosition = new Vector3(m_basePos.x, pos, m_basePos.y);
    }

    //scroll to next number
    IEnumerator ScrollToNext()
    {
        float targetPos;
        //check if we have reached end of our line (number 9)
        if (m_current == 9)
        {
            //set our line into position for reset
            SetPosition(m_posReset);
            
            m_current = -1;
        }
        
        //set target position to next number's position
        targetPos = m_posY[m_current + 1];
        
            
        //set flag
        bool isFinished = false;
        while (!isFinished)
        {
            float newPos = m_tran.localPosition.y - m_speed * Time.deltaTime;
            if (newPos <= targetPos)
            {
                SetPosition(targetPos);
                m_current++;
                break; //break the loop
            }
            else
                SetPosition(newPos);
                
            yield return null;
        }
        //Debug.Log("Scroll To Next finished with new number: " + m_current.ToString());
    }

    //scroll coroutine (towards desired number)
    IEnumerator Scroll(int number)
    {
        //play particles system
        m_ps.Play();
        //do two full scrolls
        for (int i = 0; i < 10; i++)
        {
            //Debug.Log("Calling scroll to next!");
            yield return StartCoroutine("ScrollToNext");
        }
        //now going after desired number
        while (m_current != number)
        {
            yield return StartCoroutine("ScrollToNext");
        }
        //stop particle system in the end
        m_ps.Stop();


    }

    public void SetNumber(int number)
    {
        IEnumerator cor = Scroll(number);
        StartCoroutine(cor);
    }

    
}
