﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public SpriteRenderer outline;

    public bool canInteract;
    public GameObject task;
    public int taskIndex;
    public bool activated;
    

    public void Start()
    {
        canInteract = false;
        activated = true;
        
    }

    public void Update()
    {
        

        if(canInteract)
        {
            if(Input.GetKeyDown("e"))
            {
                canInteract = false;
                outline.enabled = false;
                Debug.Log("interacted");

                //summon the appropriate task

                task.gameObject.SetActive(true);

                if (taskIndex == 0)
                {
                    //summon punchin task
                    task.GetComponent<PunchInTask>().SummonTask();
                    

                }
                else if (taskIndex == 1)
                {
                    //summon keycode task
                    task.GetComponent<CodeTask>().SummonTask();

                }
                else if (taskIndex == 2)
                {
                    //till task
                    //will add this later
                    
                }



            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (activated)
        {

            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("interact with task");
                outline.enabled = true;
                canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("goodbye");
            outline.enabled = false;
            canInteract = false;
        }
    }
}
