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
    public DoorController shopdoors;
    public TaskTrigger punchinnode;
    public GameObject endscreen;
    public GameObject tasklist;
    public bool signedin;

    public GameObject tabnotif;
    public TaskTrigger node0, node1, node2, node3, node4;





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
        shopdoors.MoveDoors();
    }

    public void Interrupt()
    {
        //each index represents the task

        
        

        if(_player.interrupted)
        {
            return;
        }

        if (!_player.busy)
        {
            return;
        }
        

        


        if (activetaskindex == 0)
        {
            activetask.GetComponent<PunchInTask>().DeSummonTask();
        }
        else if (activetaskindex == 1)
        {
            activetask.GetComponent<CodeTask>().DesummonTask();
        }

        else if (activetaskindex == 2)
        {
            activetask.GetComponent<MopTask>().DesummonTask();
        }




    }

    public void NotifyTasks()
    {
        node1.activated = true;
        node2.activated = true;
        node3.activated = true;
        node4.activated = true;
        tabnotif.SetActive(true);
    }


    public void Update()
    {

        if (signedin)
        {
            
            if (Input.GetKeyDown("tab"))
            {
                
                if(tabnotif.activeSelf == true)
                {
                    tabnotif.SetActive(false);
                }


                tasklist.SetActive(true);
                
                
            }
        }

        if (Input.GetKeyUp("tab"))
        {
            tasklist.SetActive(false);
        }
        
        
        if (completedtask != 0)
        {
            if (completedtask >= tasknumber && canleave == false)
            {
                canleave = true;
                punchinnode.activated = true;
                punchinnode.outline.enabled = true;

            }
        }
    }

    public void End( int conclusion)
    {

        //conclusion = 0 means a win
        //conclusion = 1 means a lose


        if (conclusion == 0)
        {
            endscreen.SetActive(true);
        }
        else if (conclusion == 1)
        {
            endscreen.SetActive(true);
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
