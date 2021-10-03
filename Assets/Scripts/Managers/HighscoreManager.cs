using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        
    }

    public void AddEntry(string name)
    {
        int score = ScoreManager.instance.Score;

        //load high scores
        List<Highscore> highscores = SaveManager.instance.LoadHighscores();
        Debug.Log(highscores.Count.ToString());

        //add our score
        Highscore highscore = new Highscore(name, score);
        highscores.Add(highscore);
        //sort highscores
        highscores.Sort();

        int playerIndex = highscores.IndexOf(highscore);
        UpdateEndGameDisplay(playerIndex, highscores);

        //save highscores
        SaveManager.instance.SaveHighscores(highscores);
    }

    public List<Highscore> GetTop(int number)
    {
        //load high scores
        List<Highscore> highscores = SaveManager.instance.LoadHighscores();
        return highscores.GetRange(0, number);

    }

    public void UpdateEndGameDisplay(int playerIndex, List<Highscore> scores)
    {
        HighscorePanel panel = FindObjectOfType<HighscorePanel>(true); 
        var toDisplay = new List<Highscore>();

        if (playerIndex <= 3)
             toDisplay = scores.GetRange(0, 4);
        else
        {
            toDisplay = scores.GetRange(0, 3);
            toDisplay.Add(scores[playerIndex]);
        }

        if (panel != null)
            panel.DisplayHighscores(toDisplay, playerIndex - 1);
        else
            Debug.Log("Highscore panel NOT FOUND!");

    }

    
}
