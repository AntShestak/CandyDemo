using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    PanelManager m_panels;
    ScoreScroller m_scroller;

    const string m_IN_GAME_PANEL = "InGame";
    const string m_GAME_OVER_PANEL = "GameOver";
    const string m_PAUSE_PANEL = "Pause";
    const string m_HIGHSCORE_PANEL = "Highscore";


    public Text m_debugConsole;

    private void Awake()
    {
        m_panels = GetComponent<PanelManager>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        m_scroller = m_panels.GetPanel(m_IN_GAME_PANEL).GetComponentInChildren<ScoreScroller>();
        m_panels.Activate(m_IN_GAME_PANEL);
    }



    public void ScoreChanged(int score)
    {
        m_scroller.UpdateScore(score);
    }

    //activate game over screen for player to enter name
    public void GameOver()
    {
       //set Game over text
       m_panels.Activate(m_GAME_OVER_PANEL);
    
    }

    public void ActivatePauseMenu()
    {
        m_panels.Activate(m_PAUSE_PANEL);
    }

    public void ActivateInGameMenu()
    {
        m_panels.Activate(m_IN_GAME_PANEL);
    }
    

    //set highscores panel
    public void ShowHighscoresPanel()
    {
        m_panels.Activate(m_HIGHSCORE_PANEL);
    }


    public void DebguConsole(string text)
    {
        m_debugConsole.text += $"\n{text}";
    }

}
