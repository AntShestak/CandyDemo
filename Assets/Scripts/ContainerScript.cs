using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to work with image

public class ContainerScript : MonoBehaviour
{
    public GameObject m_uiCandy;
    public GameObject m_fire;

    private PlayerController m_player; //reference to player script
    private ScoreManager m_scoreManager; //reference to score manager script
    private GameManager m_game; //reference to game manager component
    private Spawner m_spawner; //spawner reference
    public Sprite m_defaultSprite;
    private List<UICandyScript> m_candies = new List<UICandyScript>();
    private int m_iterator = 0; //iterator to keep track of current slot

    // Start is called before the first frame update
    void Start()
    {
        //find player reference
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //find gamecontroller
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        //obtain reference for Score manager
        m_scoreManager = gameController.GetComponent<ScoreManager>();
        //obtain game manager reference
        m_game = gameController.GetComponent<GameManager>();
        //obtain spawner script reference
        m_spawner = gameController.GetComponent<Spawner>();

        m_defaultSprite = m_uiCandy.GetComponent<Image>().sprite;
        //set up start position
        float startY = -56.0f;
        //spawn 10 candy images
        for (int i = 0; i < 10; i++)
        {
            //calculate position
            Vector3 spawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
            //spawn object
            GameObject ui_candy = GameObject.Instantiate(m_uiCandy, spawnPosition, Quaternion.identity);
            ui_candy.transform.parent = gameObject.transform;
            //set scale of the candy
            ui_candy.transform.localScale = new Vector3(0.4f, 0.4f, 1.0f); //startY + i * 6.0f
            ui_candy.transform.localPosition = new Vector3(0.0f , startY - i * 36.0f, 0.0f);
            ui_candy.GetComponent<Image>().color = new Color(1, 1, 1, .2f);
            //add to the list
            m_candies.Add(ui_candy.GetComponent<UICandyScript>());
        }
        //reverse list to have access from bottom but have the candies rendered on top of each other correctly
        m_candies.Reverse();
    }

    public void AddCandy(int type)
    {
        //check which type of candy we have
        if (type <= 4)
        {
            //NORMAL candy
            Sprite spr = m_spawner.GetSpriteOfType(type);
            //access candy using iterator
            m_candies[m_iterator].SetType(type, spr);
            //have we got enough candies for cheack
            if (m_iterator >= 2)
            {
                //check if three candies are the same
                bool isSame = true; //flag
                int temp = m_iterator; //temp iterator
                                       //comparison logic differs if current type is mixed candy(type 4)
                if (type != 4)
                {
                    //loop through 2 previous candies
                    for (int i = 1; i < 3; i++)
                    {
                        int prevType = m_candies[temp - i].GetCandyType();

                        if (type != prevType && prevType != 4)
                            isSame = false;
                    }
                }
                else
                {
                    //if current type is 4 then we check only two previous candies if they match or one of them is type 4
                    int p = m_candies[m_iterator - 1].GetCandyType();// previous candy
                    int pp = m_candies[m_iterator - 2].GetCandyType(); //prev prev candy
                    //check if any of them is equal to 4 or they are same
                    if (p != 4 && pp != 4 && p != pp)
                        isSame = false;
                }

                //check if isSame is still true
                if (isSame)
                {
                    Invoke("ComboRemove", 0.8f);
                }
            }
            //candies are not same
            //increase iterator
            m_iterator++;
            //check if belly is full
            if (m_iterator > 11)
                //set game over
                m_game.GameOver();
        }
        else if (type == 5)
        {
            //we have fire candy here
            BurnContainer();
        }
        else if (type == 6)
        {
            //ice candy picked up
            m_player.Freeze();
        }
           
        
    }

    void BurnContainer()
    {
        Debug.Log("FIRE!!");
        for (int i = 0; i < m_iterator; i++)
        {
            //going through active candies
            //set fire at that position
            m_candies[i].SetOnFire();
        }
        //reset iterator
        m_iterator = 0;
    }

    //function to add points and remove 3 candies
    void ComboRemove()
    {
        //Here we got the 3X combo---------------
        
        //add score
        for (int j = 0; j < 3; j++)
        {
            m_iterator--; //should be decresed as it's already on next available position
            //change candy back to grey
            m_candies[m_iterator].SetType(0,m_defaultSprite);
        }

        m_scoreManager.AddPoints(100);
       
    }
   
}
