using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public float touchTime;
    private Rigidbody myRigidbody;
    
    private bool beginTouch, tap, isDragging, mouseUp;
    private float downClickTime;
    private float ClickDeltaTime = 0.2F;
    public bool Tap
    {
        get { return tap; }
    }
    
    public bool Dragging
    {
        get { return isDragging; }
    }
    
    
    
    private Vector2 startTouch, swipeDelta;
    

    private void Update()
    {
        tap = false;

#if UNITY_EDITOR
        EditorInput();
#endif

#if UNITY_ANDROID
        MobileInput();
#endif

        
        
        //Calculate the distance
        swipeDelta = Vector2.zero;
        if (beginTouch)
        {
            touchTime += Time.deltaTime;
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2) Input.mousePosition - startTouch;
            }
            if (touchTime > 0.2f)
            {
                isDragging = true;
            }
        }
        
    }

    public void EditorInput()
    {
        #region Standalone Inputs

        if (Input.GetMouseButtonDown(0))
        {
            downClickTime = Time.time;
            beginTouch = true;
            startTouch = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }

        #endregion
    }

    public void MobileInput()
    {
        #region Mobile Inputs

        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                beginTouch = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }

        #endregion
    }

    private void Reset()
    {
        if (Time.time - downClickTime <= ClickDeltaTime)
        {
            tap = true;
        }
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        beginTouch = false;
        touchTime = 0;
    }
}
