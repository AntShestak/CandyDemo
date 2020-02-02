using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Animator m_anim; //animator reference
    private Quaternion m_baseRotation; //starting rotation

    // Start is called before the first frame update
    void Start()
    {
        //get reference for animator component
        m_anim = GetComponent<Animator>();
        //save base rotation
        m_baseRotation = transform.rotation;
    }

    public void Move()
    {
        //reset stand trigger
        m_anim.ResetTrigger("Stand");
        //set walk trigger
        m_anim.SetTrigger("Run");
        //Debug.Log("PLAYER::ACTION::RUN");
    }

    public void Stand()
    {
        //reset stand trigger
        m_anim.ResetTrigger("Run");
        //set stand trigger
        m_anim.SetTrigger("Stand");
        //Debug.Log("PLAYER::ACTION::STAND");
    }

    //rotates to face right
    public void FaceRight()
    {
        //set base rotation (faces right by default)
        transform.rotation = m_baseRotation;
    }

    //rotates to face left
    public void FaceLeft()
    {
        //rotate 180 degrees on y axis from base rotation
        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
    }
}
