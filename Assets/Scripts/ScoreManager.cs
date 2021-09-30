using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private AudioControlScript m_audio; //reference for audio control script
    private CanvasController m_canvas; //reference to canvas controller
    private int m_score = 0;

    private void Awake()
    {
        instance = this;
    }

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

    private void SaveHighscores()
    {
        List<Highscore> highscores = new List<Highscore>();

       
    }

    public void GameOver()
    {
        
        //now call to canvas and wait for return of the name
        m_canvas.GameOver(m_score);
        
    }

    //is called by the input field
    public void SetPlayerName(string name)
    {
        
        SortHighscores(name);
    }

    private void SortHighscores(string playerName)
    {
        //load high scores
        List<Highscore> highscores = SaveManager.instance.LoadHighscores();
        Debug.Log(highscores.Count.ToString());

        //add our score
        Highscore highscore = new Highscore(playerName, m_score);
        highscores.Add(highscore);
        //sort highscores
        highscores.Sort();

        //save highscores
        SaveManager.instance.SaveHighscores(highscores);

        //create a list of four for our highscore table
        List<Highscore> toCanvas = new List<Highscore>();
        //now find our position
        int playerPosition = highscores.IndexOf(highscore) + 1;
        Debug.Log("New score entry name: " + playerName + " : position" + playerPosition.ToString());
        //set top 3
        for (int i = 0; i < 3; i++)
        {
            if (i < highscores.Count)
            {
                
                toCanvas.Add(highscores[i]);
            }
        }
        //now 4th is etiher player or just 4th highcore
        if (playerPosition <= 3)
        {
            if (highscores.Count >= 4)
                toCanvas.Add(highscores[3]); //add 4th highscore because player is already on the list
        }
        else
            toCanvas.Add(highscores[playerPosition - 1]);

        //call canvas
        m_canvas.ShowHighscoresPanel(toCanvas, playerPosition);    
    }

}





