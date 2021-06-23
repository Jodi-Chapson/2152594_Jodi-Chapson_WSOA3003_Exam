using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState { PATROL, TRACK, SEARCH, IDLE }
public class BasicEnemy : MonoBehaviour
{
    //script where I attempt to code the basic enemy patrol

    [Header("References")]
    public Rigidbody2D susanrb;
    
    
    public float speed;
    public float stall;
    public AIState state;
    public Transform target;
    public Vector3 playerposition;
    public Transform one, two, three, four;
    public float walkspeed, chasespeed;
    
    public Transform[] nodes;
    public List<Transform> AvailableNodes = new List<Transform>();
    public LayerMask raymask;
    
    


    public void Start()
    {
        
        
        
        state = AIState.IDLE;
        Scan();
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


        if (susanrb.transform.position == target.transform.position || susanrb.transform.position == playerposition)
        {
            if (state != AIState.IDLE)
            {
                StartCoroutine(Pause());
                state = AIState.IDLE;
            }
        }
    }

    public void ChangeNodeTarget()
    {
        //changing the target node to a different one - and one that is not the one the enemy is currently at
        // may edit this later to include the players last known location

        if (state != AIState.TRACK)
        {
            target = AvailableNodes[Random.Range(0, AvailableNodes.Count)];

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

    public IEnumerator Pause ()
    {
        //where she stops and contemplates life for a moment
        
        
        yield return new WaitForSeconds(stall);
        AvailableNodes.Clear();


        if (state != AIState.TRACK)
        {
            Scan();
        }
        
    }


    public void Track (Vector3 playerpos)
    {
        

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

    



}



