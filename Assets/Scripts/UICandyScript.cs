using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICandyScript : MonoBehaviour
{
    public int m_type;

    private ParticleSystem m_ps;
    private Sprite m_defSprite;
    private IEnumerator cor;

    void Start()
    {
        //get particle system component
        m_ps = GetComponentInChildren<ParticleSystem>();
        m_defSprite = GetComponent<Image>().sprite;
    }

    private IEnumerator SetTypeCor(int type, Sprite spr)
    {
        float speed = 1.25f; //transformation speed
        //save normal size
        Vector3 normalSize = transform.localScale;
        //first minimize candy
        while (true)
        {
            float newSize = transform.localScale.x - Time.deltaTime * speed;
            if (newSize <= 0)
            {
                newSize = 0;
                transform.localScale = new Vector3(newSize, newSize, normalSize.z);
                break;
            }
            transform.localScale = new Vector3(newSize, newSize, normalSize.z);
            yield return null;
        }
        //apply changes
        m_type = type;

        GetComponent<Image>().sprite = spr;

        if (type == 0)
        {
            //0 is default sprite so it should be semi transparent
            GetComponent<Image>().color = new Color(1, 1, 1, .2f);
        }
        else
        {
            //set it to full color
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1.0f);
        }
        //grow
        while (true)
        {
            float newSize = transform.localScale.x + Time.deltaTime * speed;
            if (newSize >= normalSize.x)
            {
                newSize = normalSize.x;
                transform.localScale = new Vector3(newSize, newSize, normalSize.z);
                break;
            }
            transform.localScale = new Vector3(newSize, newSize, normalSize.z);
            yield return null;
        }
    }

    public void SetType(int type, Sprite spr)
    {
        cor = SetTypeCor(type, spr);
        StartCoroutine(cor);
       
    }

    public int GetCandyType()
    {
        return m_type;
    }


    public void SetOnFire()
    {
        //play the particle system
        m_ps.Play();
        ResetCandy();
        //Invoke("ResetCandy", 1.5f);
    }

    void ResetCandy()
    {
        //set type to 0 
        SetType(0, m_defSprite);
    }
}
