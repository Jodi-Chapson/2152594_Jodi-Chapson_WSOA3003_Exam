using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState { PATROL, TRACK, SEARCH, IDLE }
public class BasicEnemy : MonoBehaviour
{
    //script where I attempt to code the basic enemy patrol

    [Header("References")]
    public Rigidbody2D susanrb;
    public TaskManager tmanager;
    public Player player;
    public bool isBoss;
    public BasicEnemy boss;
    
    
    
    public float speed;
    public float stall;
    public AIState state;
    public Transform target;
    public Vector3 playerposition;
    public Transform one, two, three, four;
    public float walkspeed, chasespeed;
    public GameObject FOV;
    public Vector3 movementdirection;
    public float rotationspeed;
    
    public Transform[] nodes;
    public List<Transform> AvailableNodes = new List<Transform>();
    public LayerMask raymask;
    
    


    public void Start()
    {
        
        
        
        state = AIState.IDLE;

        if (!isBoss)
        { 
            Scan(); 
        }

    }

    public void Scan()
    {
        //cast a ray to all the nodes and keep track of which nodes are visible
        //puts all the values into the list 

        
        
        foreach (Transform nododo in nodes)
        {
            int rayhit = 0;
            
            LayerMask raymask = LayerMask.GetMask("Level");

            
            //shoots out four raycast from the positions at the enemy feet to target position

            RaycastHit2D hit, hit2, hit3, hit4;

            var direction = nododo.position - one.transform.position;
            hit = Physics2D.Raycast(one.position, direction, Vector2.Distance(nododo.position, one.position), raymask);

            if (hit.collider == null)
            {
                rayhit += 1;
            }

            direction = nododo.position - two.transform.position;
            hit2 = Physics2D.Raycast(two.position, direction, Vector2.Distance(nododo.position, two.position), raymask);

            if (hit2.collider == null)
            {
                rayhit += 1;
            }


            direction = nododo.position - three.transform.position;
            hit3 = Physics2D.Raycast(three.position, direction, Vector2.Distance(nododo.position, three.position), raymask);

            if (hit3.collider == null)
            {
                rayhit += 1;
            }

            direction = nododo.position - four.transform.position;
            hit4 = Physics2D.Raycast(four.position, direction, Vector2.Distance(nododo.position, four.position), raymask);

            if (hit4.collider == null)
            {
                rayhit += 1;
            }


            if (rayhit == 4 && state != AIState.TRACK)
            {
                AvailableNodes.Add(nododo);
            }





            

        }
        
        
        ChangeNodeTarget();
    }

    public void Update()
    {
        if (state == AIState.PATROL)
        {
            speed = walkspeed;
        }
        else if (state == AIState.TRACK)
        {
            speed = chasespeed;
        }
        else if (state == AIState.IDLE)
        {
            speed = 0;
        }


        if (state == AIState.PATROL)
        {
            //enemy will move towards the target

            susanrb.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if(state == AIState.TRACK)
        {
            //enemy will chase player
            susanrb.transform.position = Vector3.MoveTowards(transform.position, playerposition, speed * Time.deltaTime);
        }

        if (state != AIState.IDLE)
        { 
            if (susanrb.transform.position == target.transform.position && state == AIState.PATROL)
        {
            if (state != AIState.IDLE)
            {
                StartCoroutine(Pause(1));
                state = AIState.IDLE;
            }
        }

        
        
            if (state == AIState.TRACK && susanrb.transform.position == playerposition)
            {
                if (state != AIState.IDLE)
                {
                    StartCoroutine(Pause(5));
                    state = AIState.IDLE;
                }
            }
        }

        TurnEyes();
    }

    public void ChangeNodeTarget()
    {
        //changing the target node to a different one - and one that is not the one the enemy is currently at
        // may edit this later to include the players last known location

        if (state != AIState.TRACK)
        {
            if (AvailableNodes.Count != 0)
            {

                target = AvailableNodes[Random.Range(0, AvailableNodes.Count)];
            }
            else
            {
                //for the very rare occurance that Susan gets stuck in a shelf or a wall 
                //she will dislodge herself and go to the nearest node point
                Debug.Log("oops");

                int currentclosestindex = 0;
                float closestdistance = 100;
                
                
                for (int i = 0; i < nodes.Length; i++)
                {
                    float dst = Vector3.Distance(susanrb.position, nodes[i].position);

                    if (i == 0)
                    {
                        currentclosestindex = i;
                        closestdistance = dst;
                    }
                    else if (i > 0)
                    {
                        if(dst < closestdistance)
                        {
                            closestdistance = dst;
                            currentclosestindex = i;
                        }
                    }


                }

                target = nodes[currentclosestindex];


                //for (int i = 0; i < targetsInViewRadius.Length; i++)

            }

            if (susanrb.transform.position == target.transform.position)
            {
                ChangeNodeTarget();
            }
            else
            {
                state = AIState.PATROL;
            }
        }
    }

    public IEnumerator Pause (int modifier)
    {
        //where she stops and contemplates life for a moment
        
        
        yield return new WaitForSeconds(stall * modifier);
        AvailableNodes.Clear();


        if (state != AIState.TRACK)
        {
            Scan();
        }
        
    }

    


    public void Track (Vector3 playerpos)
    {
        if (!isBoss)
        {
            boss.Track(playerpos);
        
        }
        
        //target.position = playerpos;

        //casts 4 rays from the feet position of Susan to the player, to see if she can follow

        int rayhit = 0;
        RaycastHit2D hit, hit2, hit3, hit4;

        var direction = playerpos - one.transform.position;
        hit = Physics2D.Raycast(one.position, direction, Vector2.Distance(playerpos, one.position), raymask);

        if (hit.collider == null)
        {
            rayhit += 1;
        }

        direction = playerpos - two.transform.position;
        hit2 = Physics2D.Raycast(two.position, direction, Vector2.Distance(playerpos, two.position), raymask);

        if (hit2.collider == null)
        {
            rayhit += 1;
        }


        direction = playerpos - three.transform.position;
        hit3 = Physics2D.Raycast(three.position, direction, Vector2.Distance(playerpos, three.position), raymask);

        if (hit3.collider == null)
        {
            rayhit += 1;
        }

        direction = playerpos - four.transform.position;
        hit4 = Physics2D.Raycast(four.position, direction, Vector2.Distance(playerpos, four.position), raymask);

        if (hit4.collider == null)
        {
            rayhit += 1;
        }


        if (rayhit == 4)
        {
            playerposition = playerpos;
            state = AIState.TRACK;
            
        }







    }

    public void TurnEyes()
    {
        //this is supposed to turn Susans eye direction to match her movement direction (i.e fov)

        //movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized;

        if (state == AIState.PATROL)
        {
            movementdirection = new Vector3(target.transform.position.x - susanrb.transform.position.x, target.transform.position.y - susanrb.transform.position.y, 0).normalized;
        }
        else if (state == AIState.TRACK)
        {
            movementdirection = new Vector3(playerposition.x - susanrb.transform.position.x, playerposition.y - susanrb.transform.position.y, 0).normalized;
        }



        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, movementdirection);

        
        FOV.transform.rotation = Quaternion.RotateTowards(FOV.transform.rotation, rotation, rotationspeed * Time.deltaTime);
        
        
        



    }

    public void ReleaseBoss()
    {
        Scan();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player omg senpai");
            player = collision.gameObject.GetComponent<Player>();

            //susan will notify the boss and interrupt the player if they are busy
            tmanager.Interrupt();
            player.interrupted = true;




            if (isBoss)
            {
                tmanager.End(0);
                player.canmove = false;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.interrupted = false;
        }
            
            
            

        }
        
        
    }







