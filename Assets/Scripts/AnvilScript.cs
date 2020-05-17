using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilScript : MonoBehaviour
{
    //public GameObject m_explosion;
    public AudioClip m_clipFall;
   

    private AudioSource m_audioSource;
    private AudioControlScript m_audioScript; //general audio script to play hit animation
    private ParticleSystem[] m_ps; //particle systems

    bool m_isLanded = false; //is it landed or not
    bool m_isPlayerKilled = false; //flag

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        //play sound with slight delay
        Invoke("PlaySound", 0.25f);
        m_audioScript =  GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioControlScript>();

        //get particles
        m_ps = GetComponentsInChildren<ParticleSystem>();
    }

    void PlaySound()
    {
        m_audioSource.clip = m_clipFall;
        m_audioSource.volume = 0.05f; //make it more quiet
        m_audioSource.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check if we hit Player
        if (other.CompareTag("Player") && !m_isLanded)
        {
            //if player runs into anvil on the ground it's fine
            //Destroy(other.gameObject);
            //Kill player
            other.gameObject.GetComponent<PlayerController>().Die();
            m_isPlayerKilled = true;
            
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("Ground Hit!");
            m_audioScript.PlayAnvilHit();
            //play all particles (WWould be great to combine into one)
            for (int i = 0; i < m_ps.Length; i++)
                m_ps[i].Play();
            //set landed flag
            m_isLanded = true;
        }

        //if player was killed by falling anvil we don't destroy it 
        //(alternative set timescale to 0)
        if (!m_isPlayerKilled)
            Destroy(this.gameObject, 2.0f);
    }

}
