using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControlScript : MonoBehaviour
{
    public AudioClip m_groundHit; //sound of candy hitting the ground
    public AudioClip m_eatCandy; //sound of candy being eaten
    public AudioClip m_addScore; //sound of adding score

    private AudioSource m_audio;
    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
    }

    public void PlayGroundHit()
    {
        m_audio.clip = m_groundHit;
        m_audio.volume = 0.75f;
        m_audio.Play();
    }

    public void PlayEatCandy()
    {
        m_audio.clip = m_eatCandy;
        m_audio.volume = 0.35f;
        m_audio.Play();
    }

    public void PlayAddScore()
    {
        m_audio.clip = m_addScore;
        m_audio.volume = 1.00f;
        m_audio.Play();
    }
}
