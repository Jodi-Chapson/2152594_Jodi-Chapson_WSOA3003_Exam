using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeTask : MonoBehaviour
{
    public Text cardCode;
    public GameObject card;
    public Text inputCode;
    public TaskManager tmanager;
    public GameObject arrow, exit;
    public Transform taskpos1, taskpos2;
    public GameObject triggernode;
    public DoorController doorcontrol;
    public Player player;


    public int codelength;

    public float resetTime;
    public bool isResetting = false;
    public bool summoning;
    


    public void Start()
    {
        //this.transform.position = new Vector3(this.transform.position.x, tmanager.taskpos1.position.y, this.transform.position.z);
        tmanager.tasknumber++;

        codelength = Random.Range(4, 6);
        string code = string.Empty;


        for (int i = 0; i < codelength; i++)
        {
            code += Random.Range(1, 10);


        }

        cardCode.text = code;
        inputCode.text = "--";


        gameObject.SetActive(false);

    }

    public void Update()
    {
        //for testing purposes

        
        //if (Input.GetKeyDown("w"))
        //{
        //    SummonTask();
        //}



        if (summoning)
        {
            float yvalue = Mathf.Lerp(this.transform.position.y, taskpos2.transform.position.y - 2, tmanager.lerp * Time.deltaTime);
            this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);
        }

        

    }



    public void PressKeys (int number)
    {
        if (isResetting)
        {
            return;
        }

        if (inputCode.text == "--")
        {
            inputCode.text = string.Empty;
        }
        
        inputCode.text += number;


        if (inputCode.text == cardCode.text)
        {
            
            inputCode.text = "CORRECT";
            tmanager.completedtask++;
            tmanager.Taskcheck(1);
            triggernode.GetComponent<TaskTrigger>().activated = false;
            StartCoroutine(Reset(0));


        }
        else if (inputCode.text.Length >= codelength)
        {
            inputCode.text = "INCORRECT";
            StartCoroutine(Reset(1));
        }




    }


    public IEnumerator Reset( int conclusion)
    {
        //conclusion 0 = success
        //conclusion 1 = fail
        
        isResetting = true;
        
        
        yield return new WaitForSeconds(resetTime);


       
        
        if (conclusion == 0)
        {
            //desummon task and get the doors open :)
            inputCode.text = "--";
            doorcontrol.MoveDoors();




            DesummonTask();

        }
        else if (conclusion == 1)
        {
            inputCode.text = "--";
            
        }

        isResetting = false;
    }


    public void SummonTask()
    {
        player.busy = true;
        card.gameObject.SetActive(true);
        //card.GetComponent<Card>().summoning = true;
        
        StartCoroutine(tmanager.FadeEffect(0));
        summoning = true;
        StartCoroutine(ToggleBool(0));

        tmanager.activetaskindex = 1;
        tmanager.activetask = this.gameObject;
    }

    public void DesummonTask()
    {
        //if a task is closed
        player.busy = false;
        this.transform.position = taskpos1.position;
        StartCoroutine(tmanager.FadeEffect(1));
        
        exit.SetActive(false);
        arrow.SetActive(false);
        card.GetComponent<Card>().DeSummon();
        

        StartCoroutine(ToggleBool(1));
    }


    public IEnumerator ToggleBool(int type)
    {
        yield return new WaitForSeconds(1.5f);



        if (type == 0)
        {
            summoning = false;
            arrow.SetActive(true);
            exit.SetActive(true);
        }
        else if (type == 1)
        {
            
            card.gameObject.SetActive(false);
        }
    }


}
