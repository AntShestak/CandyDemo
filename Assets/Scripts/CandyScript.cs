using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{
    public GameObject m_explosion;

    private AudioControlScript m_audio;

    private int m_type = 0;

    private void Start()
    {
        m_audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioControlScript>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if we hit Player
        if (other.CompareTag("Player"))
        {
            //Find container controller
            ContainerController control = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ContainerController>();
            control.CandyHit(m_type);
            m_audio.PlayEatCandy();
        }
        else
        {
            GameObject ps = Instantiate(m_explosion, transform.position + Vector3.up * 0.01f, Quaternion.identity);
            ////get particle system start color module
            //ParticleSystem.MainModule main = ps.GetComponent<ParticleSystem>().main;
            //ParticleSystem.MinMaxGradient grad = new ParticleSystem.MinMaxGradient(Color.blue, Color.red);
            //main.startColor.mode = 

            m_audio.PlayGroundHit();
        }
        //Debug.Log("Time to destroy");
        //destroy candy when it hits
        Destroy(this.gameObject);
    }

    //set candy type and type dependant properties
    public void SetCandyType(int type)
    {
        //set type variable
        m_type = type;
        
    }

}
