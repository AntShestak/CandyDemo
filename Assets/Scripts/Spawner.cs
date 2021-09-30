using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject m_candyPrefab;
    public GameObject m_animCandy;
    public GameObject m_anvilPrefab;
    public Sprite[] m_sprites;
    public AnimationClip[] m_animations; //fire and ice candy animations
    public float m_spawnRange = 6f; //default values
    public float m_spawnHeight = 7f; //default values, has to be outside the screen

    int m_stage = 1; //depending on the stage spawns faster or slower
    float m_spawnTimer = 0;
    float m_lastAnvilSpawnTime = 0;

    //spawn rates
    float m_normalCandyPerc = 92.0f;
    float m_specialCandyPerc = 5.0f;

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
            if (rand < m_normalCandyPerc)
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
            else if (rand < m_normalCandyPerc + m_specialCandyPerc)
            {
                //Debug.Log("Deliver special candy");
                //spawn animated special candy
                //reset timer
                m_spawnTimer = 0;
                //get random x position  
                Vector3 pos = new Vector3(Random.Range(-m_spawnRange, m_spawnRange), m_spawnHeight, 0.0f);
                GameObject candy = Instantiate(m_animCandy, pos, Quaternion.identity);
                //set type for new candy ( 5 to 6 (7 is exclusive))
                int type = Random.Range(5, 7);
                candy.GetComponent<CandyScript>().SetCandyType(type);
                //set animation
                if (type == 6) //ice
                    candy.GetComponent<Animator>().SetTrigger("Ice");

            }
            else
            {
                //anvil won't spawn more often than 1 in 3sec
                if (Time.time - m_lastAnvilSpawnTime > 3.0)
                {
                    //reset timer
                    m_spawnTimer = 0;
                    //get random x position  
                    Vector3 pos = new Vector3(Random.Range(-m_spawnRange, m_spawnRange), m_spawnHeight, 0.0f);
                    GameObject candy = GameObject.Instantiate(m_anvilPrefab, pos, Quaternion.identity);

                    //set last spawn time
                    m_lastAnvilSpawnTime = Time.time;
                }
                
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
