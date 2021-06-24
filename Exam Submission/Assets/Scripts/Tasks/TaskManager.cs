using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int tasknumber;
    public int completedtask;
    public bool canleave;

    



    [Header("Tasks Info")]
    public Transform taskpos1, taskpos2;
    public float lerp = 2;
    public int activetaskindex;
    public GameObject activetask;



    public void Unlock()
    {
        //doors open

        Debug.Log("doors are open");
    }

    public void Interrupt()
    {
        //each index represents the task

        if (activetaskindex == 0)
        {
            activetask.GetComponent<PunchInTask>().DeSummonTask();
        }




    }




    public void Update()
    {
        if (completedtask != 0)
        {
            if (completedtask >= tasknumber)
            {
                canleave = true;
            }
        }
    }

    public void End( int conclusion)
    {

        //conclusion = 0 means a win
        //conclusion = 1 means a lose


        if (conclusion == 0)
        {

        }
        else if (conclusion == 1)
        {

        }


    }
}
