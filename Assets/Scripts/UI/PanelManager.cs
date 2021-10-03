using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PanelManager : MonoBehaviour
{
    
    //Unity doesn't know how to serialize a Dictionary
    Dictionary<string, GameObject> m_panelsDict = new Dictionary<string, GameObject>();

    #region Ditionary Serialization Workaround
    [Serializable]
    public struct Panel
    {
        public string key;
        public GameObject value;
    }

    public List<Panel> m_panels = new List<Panel> { };

    private void Awake()
    {
        foreach (var p in m_panels)
        {
            m_panelsDict.Add(p.key, p.value);
        }
    }
    #endregion

    public void Activate(string key)
    {
        DeactivateAll();
        m_panelsDict[key].SetActive(true);
    }

    private void DeactivateAll()
    {
        foreach (var key in m_panelsDict.Keys)
        {
            m_panelsDict[key].SetActive(false);
        }
    }

    public GameObject GetPanel(string name)
    {
        if (m_panelsDict.ContainsKey(name))
            return m_panelsDict[name];
        else
            return null;
    }
}

    


