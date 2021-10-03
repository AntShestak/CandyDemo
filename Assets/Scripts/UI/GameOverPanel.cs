using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_mainMenuButton;
    [SerializeField] Button m_setPlayerNameButton;
    [SerializeField] InputField m_nameInput;
    [SerializeField] Text m_gameOverScoreText;

    private void Start()
    {
        m_restartButton.onClick.AddListener(GameManager.instance.RestartGame);
        m_mainMenuButton.onClick.AddListener(GameManager.instance.GoToMenu);
        m_setPlayerNameButton.onClick.AddListener(() => HighscoreManager.instance.AddEntry(m_nameInput.text));
    }

    private void OnEnabe()
    {
        //enter name panel is active by DEFAULT
        m_gameOverScoreText.text = "You scored: " + ScoreManager.instance.Score.ToString();
    }

}
