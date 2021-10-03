using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Mode { Hold, Swipe, Tap, Gyro };
public class ModeManager : MonoBehaviour
{
    /// <summary>
    /// Mode desription:
    /// HOLD:   holding finger on the screen to make avatar move (containers switch automatically)
    /// SWIPE:  swipe to move in that direction
    /// TAP:    tap and avatar walks in that direction until another tap or border
    /// GYRO:   tilt phone to make avatar move (containers can be switched, since fingers are free)
    /// </summary>
    
    public static ModeManager instance;

    public static Action<Mode> OnModeChanged;
    public Mode CurrentMode {
        get { return m_currentMode; }

        set {
            m_currentMode = value;
            OnModeChanged?.Invoke(m_currentMode);
        } 
    }

    private Mode m_currentMode;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentMode = Mode.Hold;
    }

    
}
