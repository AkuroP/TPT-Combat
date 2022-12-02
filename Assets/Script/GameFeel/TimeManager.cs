using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdowdFactor = 0.05f;
    public float slowdownLength = 2f;
    public static TimeManager instance;

    private void Awake()
    {
        if (instance !=null) 
            return;
        else
        {
            instance = this;
        }
        
    }
    
    void Update()
    {
        Time.timeScale += (1 / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        
        print("time :" + Time.timeScale);
        
    }

    public void DoSlowmotion()
    {
        Time.timeScale = slowdowdFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
