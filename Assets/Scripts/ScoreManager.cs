using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private AudioControlScript m_audio; //reference for audio control script
    private CanvasController m_canvas; //reference to canvas controller
    private int m_score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //get reference to audio
        m_audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioControlScript>();
        //get reference to Canvas controller
        m_canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasController>();
    }

    //simple method to add points
    public void AddPoints(int points)
    {
        m_score += points;
        //play audio
        m_audio.PlayAddScore();
        //update GUI to resemble changes
        m_canvas.UpdateScore(m_score);
    }
}
