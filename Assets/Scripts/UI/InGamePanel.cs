using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{

    [SerializeField] Button m_pauseButton;

    CanvasController m_canvas;
    


    private void Awake()
    {
        m_canvas = FindObjectOfType<CanvasController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_pauseButton.onClick.AddListener(OnPauseClick);
    }

    // Update is called once per frame
    void OnPauseClick()
    {
        Debug.Log("ON PAUSE CLICK");
        GameManager.instance.SetPauseOn();
        m_canvas.ActivatePauseMenu(); 
    }
}
