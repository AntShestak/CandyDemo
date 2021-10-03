using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutController : MonoBehaviour
{
    //Touch screen specific
    private float width;
    private float height;

    private float m_spawnHeight; //height of spawning objects
    private float m_groundLevel; //level of ground
    private float m_range; //range for spawning objects

    private void Awake()
    {
        width = (float)Screen.width;
        height = (float)Screen.height;

        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        /*
         So now I deal only with 3 screen aspect ratios:
            16:9  (1.78)    Most common (between tablet and phone)
            4:3   (1.33)    Most common tablet
            3:2   (1.5)     Surface Pro
            (Other aspect ratios I will deal with later)
        */

        //obtain scrren width

        //define bounds
        m_spawnHeight = 7.0f;
        //no ground settings for now m_groundLevel
        m_range = 6f;

        //set range and height for spawner
        this.gameObject.GetComponent<Spawner>().SetRanges(m_range, m_spawnHeight);
        //set range for player
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetRange(m_range);
    }

    public float GetRange()
    { return m_range; }

    public float GetHeight()
    { return m_spawnHeight; }

}
