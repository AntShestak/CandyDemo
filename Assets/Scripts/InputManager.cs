using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
   //public PlayerController m_player; //reference to player controller
   //
   ////Touch screen specific
   //private float width;
   //private float height;
   //
   //private int m_inputMode = 0; //0 default mode
   //
   ////swipe inputs
   //private Vector2 swipe_startPos;
   //
   //// Start is called before the first frame update
   //void Start()
   //{
   //    //Why divided by 2????????????????
   //    width = (float)Screen.width;// / 2.0f;
   //    height = (float)Screen.height;// / 2.0f;
   //
   //}
   //
   //// Update is called once per frame
   //void Update()
   //{
   //    //if player is alive
   //    if (!m_player.IsDead())
   //    {
   //        //depending on input mode
   //        switch (m_inputMode)
   //        {
   //            //normal input mode
   //            case 0:
   //                HoldingTypeInputs(); ; //run update
   //                break;
   //            case 1:
   //                GyroInputs();
   //                break;
   //            case 2:
   //                SwipeInputs();
   //                break;
   //            default:
   //                HoldingTypeInputs();
   //                break;
   //        }
   //    }  
   //}
   //
   //void SwipeInputs()
   //{
   //    if (Input.touchSupported)
   //    {
   //        //Handling Touch inputs
   //        if (Input.touchCount > 0)
   //        {
   //
   //            Touch touch = Input.GetTouch(0);
   //
   //            // Handle finger movements based on touch phase.
   //            switch (touch.phase)
   //            {
   //                // Record initial touch position.
   //                case TouchPhase.Began:
   //                    swipe_startPos = touch.position;
   //                    break;
   //
   //               // // Determine direction by comparing the current touch position with the initial one.
   //               // case TouchPhase.Moved:
   //               //     direction = touch.position - startPos;
   //               //     break;
   //
   //                // Report that a direction has been chosen when the finger is lifted.
   //                case TouchPhase.Ended:
   //                    float deltaX = touch.position.x - swipe_startPos.x;
   //                    //set direction value should grow with amplitude of delta
   //                    float dir = deltaX / width * 2f;
   //                    m_player.SetDirection(dir);
   //                    break;
   //            }
   //        }
   //    }
   //}
   //
   ////called from update for gyre inputs
   //void GyroInputs()
   //{
   //    //pass to player info from x axis
   //    m_player.SetDirection(Input.acceleration.x);
   //}
   //
   //void HoldingTypeInputs()
   //{
   //    if (Input.touchSupported)
   //    {
   //        //Handling Touch inputs
   //        if (Input.touchCount == 1)
   //        {
   //            //i want to work with the last touch (touchCount -1)
   //            Touch touch = Input.GetTouch(Input.touchCount - 1);
   //            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
   //
   //            m_player.SetHold(touchPos.x);
   //          
   //        }
   //        else
   //            m_player.StopHold();
   //    }
   //    else
   //    {
   //        //Handling mouse input
   //        if (Input.GetMouseButton(0))
   //        {
   //            //get mouse position in world
   //            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
   //            //set it as target position (only moving on X axis).
   //            m_player.SetDirectionFromTouchPoint(mousePos);
   //        }
   //    }
   //}
   //
   ////used by debug interface
   //public void SetInputMode(int type)
   //{ m_inputMode = type; }
}


