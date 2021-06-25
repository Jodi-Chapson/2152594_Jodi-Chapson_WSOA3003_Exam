using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunchInTask : MonoBehaviour
{
    public TaskManager tmanager;
    public float countdown;
    public float maxTimer;
    public GameObject arrow, exitbutton;
    public Text scantext;
    public GameObject card;
    public Transform taskpos1;
    public Transform taskpos2;
    public Player player;
    public bool desummoning;
    
    public GameObject triggernode;

    public bool summoning;
    

    public bool countup;
    void Start()
    {
        countup = false;
        //this.transform.position = tmanager.taskpos1.position;
        tmanager.tasknumber++;
        this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("w"))
        //{
        //    SummonTask();
        //}
        

        if (countup)
        { countdown += Time.deltaTime; }


        if (countdown >= maxTimer && countup == true)
        {
            countup = false;
            countdown = 0;


            if (!tmanager.canleave)
            {
                scantext.text = "WELCOME :)";
                tmanager.signedin = true;
                triggernode.GetComponent<TaskTrigger>().activated = false;
                StartCoroutine(EndTask());

                tmanager.NotifyTasks();
            }
            else if (tmanager.canleave)
            {
                scantext.text = "GOODBYE :)";
                triggernode.GetComponent<TaskTrigger>().activated = false;

                StartCoroutine(EndTask());
                tmanager.Unlock();
            }
        }

        if (summoning && !desummoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, taskpos2.transform.position.y, tmanager.lerp * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);
        }

        



        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Card")
        {
            countup = true;


            scantext.text = "SCANNING...";
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag== "Card")
        {
            countup = false;
            countdown = 0;
            
        }
    }


    public void SummonTask()
    {
        player.busy = true;
        card.gameObject.SetActive(true);
        StartCoroutine(tmanager.FadeEffect(0));
        summoning = true;
        StartCoroutine(ToggleBool(0));

        tmanager.activetaskindex = 0;
        tmanager.activetask = this.gameObject;
        

    }

    public IEnumerator ToggleBool( int type )
    {
        yield return new WaitForSeconds(1.5f);



        if (type == 0)
        {
            summoning = false;
            arrow.SetActive(true);
            exitbutton.SetActive(true);
        }
        else if (type == 1)
        {
            desummoning = false;
            card.gameObject.SetActive(false);
        }
    }

    

    public void DeSummonTask()
    {
        player.busy = false;
        desummoning = true;
        this.transform.position = taskpos1.position;
        
        
        
        //if a task is closed
        StartCoroutine(tmanager.FadeEffect(1));
        
        exitbutton.SetActive(false);
        arrow.SetActive(false);
        card.GetComponent<Card>().DeSummon();
        scantext.text = "SCAN HERE";

        StartCoroutine(ToggleBool(1));

        
        


    }

    public IEnumerator EndTask()
    {
        player.busy = false;
        this.transform.position = taskpos1.position;

        StartCoroutine(tmanager.FadeEffect(1));

        card.GetComponent<Card>().DeSummon();
        
        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(1f);

        
        scantext.text = "SCAN HERE";
        exitbutton.SetActive(false);
        arrow.SetActive(false);


        tmanager.completedtask++;
        tmanager.Taskcheck(0);
        card.gameObject.SetActive(false);
        

        //pokes the task manager
    }
}
