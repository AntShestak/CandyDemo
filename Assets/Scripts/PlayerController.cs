using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float m_movementSpeed = 0.1f; //movement speed of player object
    public float m_positionRange = 6.0f; //the furthest point on X axis player can go

    public bool m_isDead = false;

    Rigidbody2D m_rigid;    //reference to rigidbody component of Player
    PlayerAction m_action;  //reference to action script
    SpriteRenderer m_rend;  //reference to renderer component

    //private bool m_isTapAllowed = false;
    //private bool m_isSwipeAllowed = false;
    //private bool m_isGyroAllowed = false;
    private bool m_isHolding = false; //ONLY working with one mode atm

    //float m_xTarget;        //target position on X axis
    //float m_xStart;         //start of movement for interpolation
    //float m_speedHold = 17.5f;
    //float m_speedBase = 15f;
    ////Vector2 m_s;
    ////Vector2 m_t;
    //float m_mi = 1;         //move interpolator
    //bool m_isStoped = true; //flag boolean
    //
    //
    //Vector2 m_direction; 
    //bool m_usingDirection = false;
    //bool m_usingHold = false;

    IEnumerator m_corButtonHold;


    public CanvasController m_canvas;

    private void Awake()
    {
        m_isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        m_rigid = GetComponent<Rigidbody2D>();
        //get player action script reference
        m_action = GetComponent<PlayerAction>();
        //get player action script reference
        m_rend = GetComponent<SpriteRenderer>();
        
        //set target X position to current position
        //m_xTarget = transform.position.x;
        //m_xStart = transform.position.x;

        
    }

    private void OnEnable()
    {
        TouchInputManager.OnTap += Tap;
        TouchInputManager.OnSwipe += Swipe;
        TouchInputManager.OnRelease += Release;
        //ModeManager.OnModeChanged += OnModeChange;
    }

    private void OnDisable()
    {
        TouchInputManager.OnTap -= Tap;
        TouchInputManager.OnSwipe -= Swipe;
        TouchInputManager.OnRelease -= Release;
        //ModeManager.OnModeChanged -= OnModeChange;
    }

    private void OnModeChange(Mode mode)
    {
        switch (mode)
        {
            //case Mode.Hold:
            //    m_isGyroAllowed = false;
            //    break;
            default:
                break;
        }
    }

    private void Tap(Vector2 pos)
    {
        
        IEnumerator cor = TapDetected(pos);
        StartCoroutine(cor);
    }

    private void Swipe(Vector2 dir)
    {
        
    }

    private void Release(Vector2 pos)
    {
        //Debug.Log("Release");
        m_isHolding = false;
        
    }

    IEnumerator TapDetected(Vector2 tapPos)
    {
        m_canvas.DebguConsole("Awaiting Finish");
        //current solution to handle to finger controls
        m_isHolding = false;
        yield return m_corButtonHold;
        m_canvas.DebguConsole("Starting new");
        m_corButtonHold = Hold(tapPos.x);
        StartCoroutine(m_corButtonHold);

    }

    IEnumerator Hold(float xPos)
    {
        m_canvas.DebguConsole("Coroutine Started");

        m_isHolding = true;

        float xDirection = transform.position.x - xPos < 0 ? 1 : -1;

        if (xDirection < 0)
            m_action.FaceLeft(); 
        else
            m_action.FaceRight();
        m_action.Move();

        while (m_isHolding)
        {
            /*
             * NOTE:
             * With current settings avatar advances further after passing the position of touch, this is desirable behaviour
             */

            float x = m_rigid.position.x + xDirection * m_movementSpeed * Time.fixedDeltaTime;
            
            //dragon is not gonna move further than bounds, so call stand when movd there
            if (Mathf.Abs(x) >= m_positionRange)
            {
                x = Mathf.Clamp(x, -(m_positionRange), m_positionRange);
                break; //there's nowhere to move forward anyway
            }

            m_rigid.MovePosition(new Vector2(x, 0f));

            yield return 0;
        }

        m_action.Stand();

        m_canvas.DebguConsole("Coroutine Ended");
    }



    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    //update only if player is alive
    //    if (!m_isDead)
    //    {
    //        if (m_usingDirection)
    //        {
    //            float x = m_rigid.position.x + m_direction.x * m_speedBase * Time.fixedDeltaTime;
    //            x = Mathf.Clamp(x, -(m_positionRange), m_positionRange);
    //            m_rigid.MovePosition(new Vector2(x, 0f));
    //
    //            //dragon is not gonna move further than bounds, so call stand when movd there
    //            if (Mathf.Abs(x) >= m_positionRange)
    //            {
    //                m_action.Stand();
    //            }
    //
    //
    //        }
    //        else if (m_usingHold)
    //        {
    //
    //            float x = m_rigid.position.x + m_direction.x * m_speedHold * Time.fixedDeltaTime;
    //            x = Mathf.Clamp(x, -(m_positionRange), m_positionRange);
    //            m_rigid.MovePosition(new Vector2(x, 0f));
    //
    //            //dragon is not gonna move further than bounds, so call stand when movd there
    //            if (Mathf.Abs(x) >= m_positionRange)
    //            {
    //                m_action.Stand();
    //            }
    //        }
    //    }       
    //}
    //
    //
    //
    //
    
    
    //public void SetDirection(float d)
    //{
    //    m_usingDirection = true; //moving using direction
    //    m_action.Move(); //set action to move
    //    //0.1 value is used to avoid rapid switching of sides when balancing phone (around 0)
    //    if (d < -0.1)
    //        m_action.FaceLeft();
    //    else if (d > 0.1)
    //        m_action.FaceRight();
    //   
    //
    //    m_direction = new Vector2(d, 0f);
    //}
    //
    //public void SetHold(float xStart)
    //{
    //    m_usingHold = true; //no usage of hold input type
    //    m_speedHold = m_speedBase * 0.4f; //increase m_current speed
    //    m_action.Move(); //set action to move
    //    //0.1 value is used to avoid rapid switching of sides when balancing phone (around 0)
    //    if (xStart <= transform.position.x)
    //    {
    //        m_action.FaceLeft();
    //        m_direction = new Vector2(-1, 0f);
    //    }
    //    else if (xStart > transform.position.x)
    //    {
    //        m_action.FaceRight();
    //        m_direction = new Vector2(1, 0f);
    //    }
    //         
    //}
    //
    //public void StopHold()
    //{
    //    m_speedHold = 0;
    //    m_action.Stand(); //stop running
    //}
    //
    //
    ////same as gyro but here I can easily switch direction, because d is just positive or negative 0.5
    //public void SetDirectionFromTouchPoint(Vector3 point)
    //{
    //    m_usingHold = false; //no usage of hold input type
    //    m_usingDirection = true; //moving using direction
    //    m_action.Move(); //set action to move
    //    //0.1 value is used to avoid rapid switching of sides when balancing phone (around 0)
    //    if (point.x < transform.position.x)
    //    {
    //        m_action.FaceLeft();
    //        m_direction = new Vector2(-0.5f, 0f);
    //    }
    //    else
    //    {
    //        m_action.FaceRight();
    //        m_direction = new Vector2(0.5f, 0f);
    //    }
    //}
    //
    ////freeze player 
    public void Freeze()
    {
        //Start coroutine
        StartCoroutine("Frozen");
    }
    
    IEnumerator Frozen()
    {
        Color initialColor = m_rend.color;
        float initialSpeed = m_movementSpeed;
    
        Color start = Color.blue;
        Color end = Color.white;
        Color current = start;
    
        float startSpeed = initialSpeed * 0.25f; //speed at the start of interpolation
        float endSpeed = initialSpeed;
    
        float i = 0; //interpolator
        float speed = 0.5f;
        
        //interpolate
        while (i <= 1)
        {
            //adjust color
            m_rend.color = current;
            //increase i
            i += speed * Time.deltaTime;
            //lerp color
            current = Color.Lerp(start, end, i);
            //lerp speed
            m_movementSpeed = Mathf.Lerp(startSpeed, endSpeed, i);
            yield return 0;
        }
    
        //return normal values
        m_rend.color = initialColor;
        m_movementSpeed = initialSpeed;
    
    
    }
    
    public void Die()
    {
        if (!m_isDead)
        {
            StopAllCoroutines(); //to stop player from moving
            m_action.Squeeze();
            //remove capsule collider
            GetComponent<CapsuleCollider2D>().enabled = false;
            //is dead
            m_isDead = true;
            //tell game manager
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().GameOver();

            //unsubscribe from control events
            TouchInputManager.OnTap -= Tap;
            TouchInputManager.OnSwipe -= Swipe;
            TouchInputManager.OnRelease -= Release;
        }
        
    
    }
    
    public bool IsDead()
    {
        return m_isDead;
    }

    public void SetRange(float range)
    {
        m_positionRange = range;
    }

}
