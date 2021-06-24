using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int tasknumber;
    public int completedtask;
    public bool canleave;




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
}
