using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_movementSpeed = 0.1f; //movement speed of player object
    public float m_positionRange = 6.0f; //the furthest point on X axis player can go

    Rigidbody2D m_rigid;    //reference to rigidbody component of Player
    PlayerAction m_action;  //reference to action script
    float m_xTarget;        //target position on X axis
    float m_xStart;         //start of movement for interpolation
    //Vector2 m_s;
    //Vector2 m_t;
    float m_mi = 1;         //move interpolator
    bool m_isStoped = true; //flag boolean

    Vector2 m_direction; 
    bool m_usingDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        m_rigid = GetComponent<Rigidbody2D>();
        //get player action script reference
        m_action = GetComponent<PlayerAction>();
        //set target X position to current position
        m_xTarget = transform.position.x;
        m_xStart = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_usingDirection)
        {
            float x = m_rigid.position.x + m_direction.x * 15f * Time.fixedDeltaTime;
            x = Mathf.Clamp(x, -(m_positionRange), m_positionRange);
            m_rigid.MovePosition(new Vector2(x,0f));

            //dragon is not gonna move further than bounds, so call stand when movd there
            if (Mathf.Abs(x) >= m_positionRange)
            {
                m_action.Stand();
            }
           
                
        }
        else
        {
            //Problems: Short distances moves slower, long distance very fast
            if (m_mi < 1)
            {
                //increase interpolator value
                m_mi += Time.deltaTime * m_movementSpeed;
                //adjust position
                transform.position = new Vector3(Mathf.Lerp(m_xStart, m_xTarget, m_mi), transform.position.y, transform.position.z);
                //Debug.Log("X current: " + transform.position.x.ToString() + "   | Target: " + m_xTarget.ToString());
                //Debug.Log("Interpolator: " + m_mi.ToString());
            }
            else
            {
                //check if it was already stopped
                if (!m_isStoped)
                {
                    //set action to stand
                    m_action.Stand();
                    //set flag
                    m_isStoped = true;
                }
            }
        }
    }

    //set's new target for player (only x pos is relevant)
    public void SetTarget(float xTarget)
    {
        m_usingDirection = false; //to use normal mode of movement to target

        //clamp x in range & set it as new target
        m_xTarget = Mathf.Clamp(xTarget, -(m_positionRange), m_positionRange);
        m_xStart = transform.position.x;
        //now check which way player has to turn
        if (m_xTarget > transform.position.x)
            m_action.FaceRight(); //face right
        else
            m_action.FaceLeft(); //face left
        //reset interpolator
        m_mi = 0;
        //set run animation
        m_action.Move();
        //set flag
        m_isStoped = false;
    }

    public void SetRange(float range)
    {
        m_positionRange = range;
    }

    public void SetDirection(float d)
    {
        m_usingDirection = true; //moving using direction
        m_action.Move(); //set action to move
        //0.1 value is used to avoid rapid switching of sides when balancing phone (around 0)
        if (d < -0.1)
            m_action.FaceLeft();
        else if (d > 0.1)
            m_action.FaceRight();
       

        m_direction = new Vector2(d, 0f);
    }

    //same as gyro but here I can easily switch direction, because d is just positive or negative 0.5
    public void SetDirectionFromTouchPoint(Vector3 point)
    {
        m_usingDirection = true; //moving using direction
        m_action.Move(); //set action to move
        //0.1 value is used to avoid rapid switching of sides when balancing phone (around 0)
        if (point.x < transform.position.x)
        {
            m_action.FaceLeft();
            m_direction = new Vector2(-0.5f, 0f);
        }
        else
        {
            m_action.FaceRight();
            m_direction = new Vector2(0.5f, 0f);
        }
    }
}
