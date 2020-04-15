using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject m_candyPrefab;
    public GameObject m_animCandy;
    public Sprite[] m_sprites;
    public AnimationClip[] m_animations; //fire and ice candy animations
    public float m_spawnRange = 6f; //default values
    public float m_spawnHeight = 7f; //default values, has to be outside the screen

    int m_stage = 1; //depending on the stage spawns faster or slower
    float m_spawnTimer = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update timer
        m_spawnTimer += Time.deltaTime;
        //check if its time to spawn
        if (m_spawnTimer >= Random.Range(0.80f, 1.20f))
        {
            //time to spawn, calculate random factor
            float rand = Random.Range(1.0f,100.0f);
            //90% of chance to spawn normal candy
            if (rand < 60.0f)
            {
                //reset timer
                m_spawnTimer = 0;
                //get random x position  
                Vector3 pos = new Vector3(Random.Range(-m_spawnRange, m_spawnRange), m_spawnHeight, 0.0f);
                GameObject candy = GameObject.Instantiate(m_candyPrefab, pos, Quaternion.identity);
                //set type for new candy ( 1 to 4 (5 is exclusive))
                int type = Random.Range(1, 5);
                candy.GetComponent<CandyScript>().SetCandyType(type);
                candy.GetComponent<SpriteRenderer>().sprite = m_sprites[type - 1];
            }
            else
            {
                //Debug.Log("Deliver special candy");
                //spawn animated special candy
                //reset timer
                m_spawnTimer = 0;
                //get random x position  
                Vector3 pos = new Vector3(Random.Range(-m_spawnRange, m_spawnRange), m_spawnHeight, 0.0f);
                GameObject candy = GameObject.Instantiate(m_animCandy, pos, Quaternion.identity);
                //set type for new candy ( 5 or 6))
                int type = Random.Range(5, 7);
                candy.GetComponent<CandyScript>().SetCandyType(type);
                if (type >= 6)
                    candy.GetComponent<Animator>().SetTrigger("Ice");

            }
           
        }
        
    }

    public void SetRanges(float range, float height)
    {
        m_spawnHeight = height;
        m_spawnRange = range;
    }

    //returns prite thats equivalent to to the type
    public Sprite GetSpriteOfType(int type)
    {
        return m_sprites[type - 1];
    }
}
