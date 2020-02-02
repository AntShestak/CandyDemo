using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for Image access

public class ContainerController : MonoBehaviour
{
    public GameObject m_leftBlock;
    public GameObject m_rightBlock;
    public Color m_normal;
    public Color m_highlight;


    private int m_currentBlock = 0; //0 is for left block

    // Start is called before the first frame update
    void Start()
    {
        //m_normal = Color.white;
        //m_highlight = Color.yellow;
        m_leftBlock.GetComponent<Image>().color = m_highlight;
        m_rightBlock.GetComponent<Image>().color = m_normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sends candy to the appropriate container
    public void CandyHit(int type)
    {
        //check which block we are working with
        if (m_currentBlock == 0)
        {
            //tell left block we got a candy
            m_leftBlock.GetComponent<ContainerScript>().AddCandy(type);
            //now we have finished with left block, swap sprites
            m_leftBlock.GetComponent<Image>().color = m_normal;
            m_rightBlock.GetComponent<Image>().color = m_highlight;
            //set identificator
            m_currentBlock = 1;
        }
        else
        {
            //tell left RIGHT we got a candy
            m_rightBlock.GetComponent<ContainerScript>().AddCandy(type);
            //now we have finished with right block, swap sprites
            m_leftBlock.GetComponent<Image>().color= m_highlight;
            m_rightBlock.GetComponent<Image>().color = m_normal;
            //set identificator
            m_currentBlock = 0;
        }
    }
}
