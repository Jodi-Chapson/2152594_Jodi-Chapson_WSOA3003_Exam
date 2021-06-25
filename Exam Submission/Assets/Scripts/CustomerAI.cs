using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerAI : MonoBehaviour
{
    public enum AIState { PATROL, IDLE, TRACK, PESTERING}

    [Header("References")]
    public Player player;
    public TaskManager tmanager;
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
    public float walkspeed, chasespeed;


    [Header("Pestering References")]
    public GameObject dialoguebox;
    public Text dialogue;
    public string[] dialogueoptions;
    public int[] dialoguechoices;
    public int correctchoice;
    public float pesteringCD;
    public float currentCD;
    public bool onCD;



    void Start()
    {
        
        state = AIState.IDLE;
        Scan();
        onCD = false;
    }

    // Update is called once per frame
    void Update()
    {
     
        if (currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
        else
        {
            onCD = false;
        }

        if (state == AIState.PATROL)
        {
            speed = walkspeed;
        }
        else if (state == AIState.TRACK)
        {
            speed = chasespeed;
        }
        else if (state == AIState.IDLE || state == AIState.PESTERING)
        {
            speed = 0;
        }

        if (Input.GetKeyDown("space"))
        {
            Scan();
        }

        //customer will move towards target position

        if (state == AIState.PATROL || state == AIState.TRACK)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }


        if (transform.position == target && state != AIState.PESTERING)
        {
            if (state == AIState.PATROL)
            {

                state = AIState.IDLE;
                StartCoroutine(Idle());
            }
            else if (state == AIState.TRACK)
            {
                state = AIState.IDLE;
                StartCoroutine(Idle());
            }
        }
        

    }

    

    public void ApproachPlayer( Vector3 playerpos)
    {

        if (state != AIState.TRACK && state != AIState.PESTERING && !onCD)
        {

            state = AIState.TRACK;
            target = playerpos;
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

        if (state != AIState.TRACK && state != AIState.PESTERING)
        {
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


    public IEnumerator PreparingPester()
    {
        state = AIState.PESTERING;
        tmanager.Interrupt();
        player.interrupted = true;
        //player.canmove = false;
        if (!player.canmove)
        {
            
            yield return new WaitForSeconds(1f);
        }
        else
        {
            player.canmove = false;
        }
        player.pesteringcustomers++;
        Pester();
    }

    public void Pester()
    {
        

        int random = Random.Range(0, 3);

        dialogue.text = dialogueoptions[random];
        correctchoice = dialoguechoices[random];
        

        dialoguebox.SetActive(true);
        

        //instantiates a chatbox 
        //with a random question
        //each paired with a random answer
        
        Debug.Log("customer being a betch :)");
    }


    public void PassOnResults(int result)
    {
        StartCoroutine(Thinking(result));
    }

    public IEnumerator Thinking(int results)
    {
        dialogue.text = "...";

        yield return new WaitForSeconds(0.25f);


        if (results == correctchoice)
        {
            dialogue.text = ":)";
            player.pesteringcustomers -= 1;

            yield return new WaitForSeconds(0.5f);


            dialogue.text = "...";
            dialoguebox.SetActive(false);
            if (player.pesteringcustomers == 0)
            {
                player.canmove = true;
            }

            currentCD = pesteringCD;
            onCD = true;
            state = AIState.IDLE;
            Scan();

        }
        else
        {
            Debug.Log("incorrect");
            Pester();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Player>();

            if (state != AIState.PESTERING)
            {
                StartCoroutine(PreparingPester());
            }
        }
    }
}
