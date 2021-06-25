using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MopTask : MonoBehaviour
{
    public int puddles;
    public int puddlesleft;
    public Player player;
    public bool summoning;
    public TaskManager tmanager;

    public Transform taskpos1, taskpos2;
    public GameObject exit;
    public GameObject triggernode;
    public GameObject text;
    public GameObject sponge;
    public bool desummoning;



    public void Start()
    {
        puddlesleft = puddles;
        tmanager.tasknumber++;

        
    }



    public void Cleaned()
    {
        puddlesleft -= 1;

        if (puddlesleft == 0)
        {
            Debug.Log("nani");
            //task completed successfully
            tmanager.completedtask++;
            tmanager.Taskcheck(2);
            text.SetActive(true);
            triggernode.GetComponent<TaskTrigger>().activated = false;

            

            DesummonTask();




        }


    }



    public void Update()
    {
        //for testing purposes


        //if (Input.GetKeyDown("w"))
        //{
        //    SummonTask();
        //}



        if (summoning && !desummoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, taskpos2.transform.position.y - 2, tmanager.lerp * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);
        }



    }



    public void SummonTask()
    {
        player.busy = true;
        sponge.SetActive(true);
        

        StartCoroutine(tmanager.FadeEffect(0));
        summoning = true;
        StartCoroutine(ToggleBool(0));

        tmanager.activetaskindex = 2;
        tmanager.activetask = this.gameObject;
    }

    public void DesummonTask()
    {
        //if a task is closed
        sponge.SetActive(false);
        player.busy = false;
        desummoning = true;
        this.transform.position = taskpos1.position;
        StartCoroutine(tmanager.FadeEffect(1));

        exit.SetActive(false);

        


        StartCoroutine(ToggleBool(1));
    }


    public IEnumerator ToggleBool(int type)
    {
        yield return new WaitForSeconds(1.5f);



        if (type == 0)
        {
            summoning = false;
            
            exit.SetActive(true);
        }
        else if (type == 1)
        {
            desummoning = false;
            
        }
    }

}
