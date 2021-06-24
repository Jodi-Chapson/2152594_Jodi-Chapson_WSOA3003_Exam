using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    public Transform longhand, shorthand;
    public float degrees;
    public bool stopped;
    public float maxTime;


    public float time;
    
    void Start()
    {
        stopped = false;
        maxTime = (240 / degrees);
    }

    
    // Update is called once per frame
    void Update()
    {
        
        
        
        
        

        if (!stopped)
        {
            longhand.transform.eulerAngles = new Vector3(0, 0, -time * degrees * 12);
            shorthand.transform.eulerAngles = new Vector3(0, 0, 90 - time * degrees);

            time += Time.deltaTime;
        }



        if (time > maxTime)
        {
            longhand.transform.eulerAngles = new Vector3(0, 0, 0);
            stopped = true; 

            //call the endgame function

        }


    }

    
}
