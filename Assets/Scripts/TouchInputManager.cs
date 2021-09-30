using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInputManager : MonoBehaviour
{
    Touch _touch, m_prevTouch;
    Vector3 _touchStart, _touchEnd;
    Camera m_cam;

    public static Action<Vector2> OnTap;        //Vector2 type argument is screen Position
    public static Action<Vector2> OnSwipe;     //Vector2 type argument is delta between start and end of the touch in scrren pixels
    public static Action<Vector2> OnRelease;

    private void Start()
    {
        m_cam = Camera.main; //cam reference
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported) 
        {
            if (Input.touchCount > 0)
            {
                //get the first detected touch
                //_touch = Input.GetTouch(0);
                //testing the last touch
                _touch = Input.GetTouch(Input.touchCount - 1);
                //if touch has just began store starting position of this touch
                if (_touch.phase == TouchPhase.Began)
                {
                    _touchStart = _touch.position;
                    TouchStarted(_touchStart);
                }
                else if (_touch.phase == TouchPhase.Stationary && !m_prevTouch.Equals(_touch))
                {
                    //Trying to fix double touch issue
                    _touchStart = _touch.position;
                    TouchStarted(_touchStart);

                    m_prevTouch = _touch; 

                }
                else if (_touch.phase == TouchPhase.Ended)
                {
                    m_prevTouch = _touch;

                    //else i touch has ended calculate if it was drag upwards
                    _touchEnd = _touch.position;
                    TouchEnded(_touchEnd);


                    //calculate delta for swipe
                    Vector2 vecDelta = _touchEnd - _touchStart;
                    //assuming below the minPix is considered as tap
                    float minPix = 20;
                    if (vecDelta.x >= minPix || vecDelta.y >= minPix)
                    {
                        OnSwipe?.Invoke(vecDelta);


                    }



                }
            }
        }
            
        else //touch not supported, using for debugging in editor
        {
            //running mouse inputs here

            if (Input.GetMouseButtonDown(0))
            {
                //get mouse position in world
                //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TouchStarted(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                //get mouse position in world
                //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                TouchEnded(Input.mousePosition);
            }
        }
    }

    private void TouchStarted(Vector3 screenPosition)
    {
        
        OnTap?.Invoke((Vector2)m_cam.ScreenToWorldPoint(screenPosition));
    }

    private void TouchEnded(Vector3 screenPosition)
    {
        OnRelease?.Invoke((Vector2)m_cam.ScreenToWorldPoint(screenPosition));
    }
}
