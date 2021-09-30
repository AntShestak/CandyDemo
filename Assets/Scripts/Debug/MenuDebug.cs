using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDebug : MonoBehaviour
{
    public GameObject debugPanel;

    public void DebugMenu()
    {
        if (debugPanel.activeSelf)
        {
            debugPanel.SetActive(false);
        }
        else
            debugPanel.SetActive(true);
    }
}
