using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public enum AIState { PATROL, IDLE}

    [Header("References")]
    public LayerMask raymask;
    public Vector3 direction;
    public Vector3 endpoint;
    public float speed;
    public Vector3 target;
    public AIState state;
    public int movedistance;
    public int rayhit;
    public Transform one, two, three, four;
    public RaycastHit2D hit, hit2;


    
    
    

    void Start()
    {
        
        state = AIState.IDLE;
        Scan();
    }

    // Update is called once per frame
    void Update()
    {
     
        
        if (Input.GetKeyDown("space"))
        {
            Scan();
        }


        



        //customer will move towards target position

        if (state == AIState.PATROL)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


        if (transform.position == target && state == AIState.PATROL)
        {
            state = AIState.IDLE;
            StartCoroutine(Idle());
        }
        

    }



    public IEnumerator Idle()
    {
        //when the customer stalls for a random amount of time ;)

        int idletimer;
        idletimer = Random.Range(1, 5);

        //Debug.Log(idletimer);

        yield return new WaitForSeconds(idletimer);

        Scan();


    }

    public void Scan()
    {
        rayhit = 0;
        
        //chooses a random direction for the customer to go in
        int random = Random.Range(0, 4);
        movedistance = Random.Range( 2, 4);
        if(random == 0)
        {
            //look up
            direction = Vector3.up;
            endpoint = new Vector3(this.transform.position.x, this.transform.position.y + movedistance, this.transform.position.z);
        }
        else if ( random == 1)
        {
            //look down
            direction = Vector3.down;
            endpoint = new Vector3(this.transform.position.x, this.transform.position.y - movedistance, this.transform.position.z);
        }
        else if (random == 2)
        {
            //look left
            direction = Vector3.left;
            endpoint = new Vector3(this.transform.position.x - movedistance, this.transform.position.y, this.transform.position.z);
        }
        else if (random == 3)
        {
            //look right
            direction = Vector3.right;
            endpoint = new Vector3(this.transform.position.x + movedistance, this.transform.position.y, this.transform.position.z);
        }


        //casts two rays depending on which direction they choose

        
        
        if (random == 0)
        {
            //look up
            hit = Physics2D.Raycast(three.position, direction, movedistance, raymask);
            hit2 = Physics2D.Raycast(four.position, direction, movedistance, raymask);

        }
        else if (random == 1)
        {
            //look down
            hit = Physics2D.Raycast(two.position, direction, movedistance, raymask);
            hit2 = Physics2D.Raycast(one.position, direction, movedistance, raymask);

        }
        else if (random == 2)
        {
            //look left
            hit = Physics2D.Raycast(four.position, direction, movedistance, raymask);
            hit2 = Physics2D.Raycast(one.position, direction, movedistance, raymask);

        }
        else
        {
            //look right
            hit = Physics2D.Raycast(two.position, direction, movedistance, raymask);
            hit2 = Physics2D.Raycast(three.position, direction, movedistance, raymask);

        }


        

        if (hit.collider == null)
        {
            rayhit += 1;
        }

        if (hit2.collider == null)
        {
            rayhit += 1;
        }

        if (rayhit == 2)
        {
            //the path is clear
            //move customer to new location
            target = endpoint;
            state = AIState.PATROL;

        }
        else
        {
            //rescan
            Scan();

        }



       

        
        
    }
}
