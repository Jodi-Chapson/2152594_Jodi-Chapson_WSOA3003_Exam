using System.Collections;
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
    public DoorController doorcontrol;
    public BasicEnemy boss;
    public Animator clockpulse;

    public bool delayed;
    public float delaytime;
    public float maxdelaytime;
    public float warningtime;


    public bool ended;
    public bool bossreleased;
    public TaskManager _taskmanager;
    public bool warn;
    void Start()
    {
        stopped = false;
        maxTime = (240 / degrees);
        bossTime = (90 / degrees);
        warningtime = 210 / degrees;
        delayed = true;
        ended = false;
        bossreleased = false;
        warn = false;
        clockpulse = this.GetComponent<Animator>();
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
            longhand.transform.eulerAngles = new Vector3(0, 0, -time * degrees * 12);
            shorthand.transform.eulerAngles = new Vector3(0, 0, 90 - time * degrees);

        }


        if (time >= bossTime && !bossreleased)
        {
            Debug.Log("boss");
            bossreleased = true;

            doorcontrol.MoveDoors();
            boss.Scan();
        }

        if (time >= warningtime && !warn)
        {
            warn = true;
            TogglePulseAnimation();

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

    public void TogglePulseAnimation()

    {
        clockpulse.enabled = true;
    }


}
