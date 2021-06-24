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
    
    public bool summoning, desummoning;
    

    public bool countup;
    void Start()
    {
        countup = false;
        this.transform.position = tmanager.taskpos1.position;
        tmanager.tasknumber++;
        
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


        if (countdown >= maxTimer)
        {
            countup = false;
            countdown = 0;


            if (!tmanager.canleave)
            {
                scantext.text = "WELCOME :)";
                StartCoroutine(EndTask());
            }
            else if (tmanager.canleave)
            {
                scantext.text = "GOODBYE :)";
                StartCoroutine(EndTask());
                tmanager.Unlock();
            }
        }

        if (summoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, tmanager.taskpos2.transform.position.y, tmanager.lerp * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);
        }

        if (desummoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, tmanager.taskpos1.transform.position.y, tmanager.lerp * Time.deltaTime);
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
        }
    }

    

    public void DeSummonTask()
    {
        //if a task is closed

        desummoning = true;
        exitbutton.SetActive(false);
        arrow.SetActive(false);
        card.GetComponent<Card>().DeSummon();
        scantext.text = "SCAN HERE";

        StartCoroutine(ToggleBool(1));

        
        


    }

    public IEnumerator EndTask()
    {
        card.GetComponent<Card>().DeSummon();
        
        yield return new WaitForSeconds(1f);
        desummoning = true;
        yield return new WaitForSeconds(1f);

        desummoning = false;
        scantext.text = "SCAN HERE";
        exitbutton.SetActive(false);
        arrow.SetActive(false);


        tmanager.completedtask++;
        

        //pokes the task manager
    }
}
