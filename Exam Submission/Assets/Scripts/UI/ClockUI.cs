﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [Header("References")]
    public Transform longhand, shorthand;
    public float degrees;
    public bool stopped;
    public float maxTime;
    public float bossTime;
    public float time;

    public bool delayed;
    public float delaytime;
    public float maxdelaytime;


    public bool ended;
    public bool bossreleased;
    public TaskManager _taskmanager;
    
    void Start()
    {
        stopped = false;
        maxTime = (240 / degrees);
        bossTime = (90 / degrees);
        delayed = true;
        ended = false;
        bossreleased = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        delaytime += Time.deltaTime;

        if (delaytime > maxdelaytime)
        {
            delayed = false;
        }
        

        if (!delayed && !stopped)
        {
            time += Time.deltaTime;
        }
        
        

        if (!stopped)
        {
            longhand.transform.eulerAngles = new Vector3(0, 0, -(int)time * degrees * 12);
            shorthand.transform.eulerAngles = new Vector3(0, 0, 90 - time * degrees);

        }


        if (time >= bossTime && !bossreleased)
        {
            Debug.Log("boss");
            bossreleased = true;
        }




        if (time >= maxTime && !ended)
        {
            ended = true;
            longhand.transform.eulerAngles = new Vector3(0, 0, 0);
            stopped = true;




            //this is a loss
            //so calls the end function with an conclusion int of lost :(
            _taskmanager.End(1);

            

        }


    }

    
}