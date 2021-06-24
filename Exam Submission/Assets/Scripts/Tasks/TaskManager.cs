using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public int tasknumber;
    public int completedtask;
    public bool canleave;
    public Animator Fade;
    public Player _player;





    [Header("Tasks Info")]

    
    
    public float lerp = 2;
    public int activetaskindex;
    public GameObject activetask;


    //task indexs:
    // 0 == punchin
    // 1 == keycode
    // 2 == till
    //etc etc, add as we go



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

    public IEnumerator FadeEffect (int type)
    {

        //type = 0 means fade to black
        // type = 1 means fade out from black


        

        if (type == 0)
        {
            _player.canmove = false;
            Fade.gameObject.SetActive(true);
            Fade.Play("FadeIn", 0, 0.0f);

            
        }
        else if (type == 1)
        {
            Fade.Play("FadeOut", 0, 0.0f);
            
            yield return new WaitForSeconds(0.5f);

            Fade.gameObject.SetActive(false);
            _player.canmove = true;



        }

        
        

    }
}
