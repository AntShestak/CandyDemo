using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public GameObject m_debugPanel; //reference to debug panel
    public GameObject m_borders; //all borders
    public GameObject m_left;
    public GameObject m_right;
    public GameObject m_top;

    public LayoutController m_layout;

    bool m_isDebugActive = true; //by default debug is active

    //method to switch debug on/off
    public void DebugSwitch()
    {
        m_isDebugActive = !m_isDebugActive;
        //if debug was turned on activate debugPanel
        if (m_isDebugActive)
        {
            m_debugPanel.SetActive(true);
            ActivateBorders();
        }
        else
        {
            m_debugPanel.SetActive(false);
            m_borders.SetActive(false);
        }
    }

    void ActivateBorders()
    {
        float range = m_layout.GetRange();
        m_left.transform.position = new Vector3(-range, 0f, 0f);
        m_right.transform.position = new Vector3(range, 0f, 0f);
        m_top.transform.position = new Vector3(0f, m_layout.GetHeight(), 0f);
        m_borders.SetActive(true);
    }

}
