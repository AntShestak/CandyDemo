using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] Button m_restartButton;
    [SerializeField] Button m_mainMenuButton;
    [SerializeField] Button m_continueButton;

    CanvasController m_canvas;

    private void Awake()
    {
        m_canvas = FindObjectOfType<CanvasController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_restartButton.onClick.AddListener(GameManager.instance.RestartGame);
        m_mainMenuButton.onClick.AddListener(GameManager.instance.GoToMenu);
        m_continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    // Update is called once per frame
    private void OnContinueButtonClick()
    {
        GameManager.instance.SetPauseOff();
        m_canvas.ActivateInGameMenu();

    }
}
