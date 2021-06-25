using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TillTask : MonoBehaviour
{
    
    public Player player;
    public bool summoning;
    public TaskManager tmanager;

    public Transform taskpos1, taskpos2;
    public GameObject exit;
    public GameObject triggernode;
    
    public GameObject item1, item2, item3;



    public void Start()
    {
        
        tmanager.tasknumber++;


    }



    public void Completed()
    {
        
        
            //task completed successfully
            tmanager.completedtask++;
        tmanager.Taskcheck(3);
        triggernode.GetComponent<TaskTrigger>().activated = false;


        DesummonTask();
            
        


    }



    public void Update()
    {
        



        if (summoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, taskpos2.transform.position.y - 2, tmanager.lerp * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);
        }



    }



    public void SummonTask()
    {
        player.busy = true;
        item1.SetActive(true);
        item2.SetActive(true);
        item3.SetActive(true);


        StartCoroutine(tmanager.FadeEffect(0));
        summoning = true;
        StartCoroutine(ToggleBool(0));

        tmanager.activetaskindex = 3;
        tmanager.activetask = this.gameObject;
    }

    public void DesummonTask()
    {
        //if a task is closed
        item1.SetActive(false);
        item2.SetActive(false);
        item3.SetActive(false);

        player.busy = false;
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


        }
    }
}
