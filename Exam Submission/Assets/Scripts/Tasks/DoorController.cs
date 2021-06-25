using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject leftdoor, rightdoor;
    public bool doorsmove;
    public Vector3 leftnewpos, rightnewpos;
    public TaskManager tmanager;

    public void Start()
    {
        doorsmove = false;
        leftnewpos = new Vector3(leftdoor.transform.position.x-1.5f, leftdoor.transform.position.y, leftdoor.transform.position.z);
        rightnewpos = new Vector3(rightdoor.transform.position.x+1.5f, rightdoor.transform.position.y, rightdoor.transform.position.z);

    }
    public void Update()
    {
        if (doorsmove)
        {
            float lerp = 2;

            float leftxvalue = Mathf.Lerp(leftdoor.transform.position.x, leftnewpos.x, lerp * Time.deltaTime);
            leftdoor.transform.position = new Vector3(leftxvalue, leftdoor.transform.position.y, leftdoor.transform.position.z);

            float rightxvalue = Mathf.Lerp(rightdoor.transform.position.x, rightnewpos.x, lerp * Time.deltaTime);
            rightdoor.transform.position = new Vector3(rightxvalue, rightdoor.transform.position.y, rightdoor.transform.position.z);


            //float yvalue = Mathf.Lerp(this.transform.position.y, taskpos2.transform.position.y, tmanager.lerp * Time.deltaTime);
            //this.transform.position = new Vector3(this.transform.position.x, yvalue, this.transform.position.z);




            //leftdoor.transform.position = Vector3.MoveTowards(leftdoor.transform.position, leftnewpos, Vector3.Distance(leftdoor.transform.position, leftnewpos));
            //rightdoor.transform.position = Vector3.MoveTowards(rightdoor.transform.position, rightnewpos, Vector3.Distance(rightdoor.transform.position, rightnewpos));

        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tmanager.End(0);
        }
    }


    public void MoveDoors()
    {
        doorsmove = true;
    }
}
