using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIState { PATROL, TRACK, SEARCH }
public class BasicEnemy : MonoBehaviour
{
    //script where I attempt to code the basic enemy patrol

    [Header("References")]
    public Rigidbody2D susanrb;
    public bool patrol;
    public bool paused;
    public float speed;
    public AIState state;
    public Transform target;
    public Transform[] nodes;
    
    


    public void Start()
    {

        ChangeNodeTarget();
        //patrol = true;
        paused = false;
        state = AIState.PATROL;

        
        
    }

    public void Scan()
    {
        //cast a ray to all the nodes and the compile while nodes are visible
        
        
        {
            var direction = target.position - susanrb.transform.position;
            LayerMask raymask = LayerMask.GetMask("Node", "Level");

            RaycastHit2D hit = Physics2D.Raycast(susanrb.position, direction, Mathf.Infinity, raymask);
            Debug.DrawRay(susanrb.position, direction, Color.green);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Node")
                {
                    Debug.Log("bingo");
                    ChangeNodeTarget();
                        
                }
                else
                {
                    Debug.Log("who dis");
                    
                    ChangeNodeTarget();
                }
            }

            //LayerMask mask = LayerMask.GetMask("Wall");

            //// Check if a Wall is hit.
            //if (Physics.Raycast(transform.position, transform.forward, 20.0f, mask))


            //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

                //RaycastHit hit;
                //// Does the ray intersect any objects excluding the player layer
                //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                //{
                //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //    Debug.Log("Did Hit");
                //}
                //else
                //{
                //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //    Debug.Log("Did not Hit");
                //}
        }
    }

    public void Update()
    {
        var direction = target.position - susanrb.transform.position;
        Debug.DrawRay(susanrb.position, direction, Color.green);


        if (Input.GetKeyDown("space"))
        {
            ///temporary thing to trigger the changing of the target node
            //patrol = true;
            //ChangeNodeTarget();
            Scan();
        }


        if (patrol)
        {
            //enemy will move towards the target
            susanrb.transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }


        if (susanrb.transform.position == target.transform.position)
        {
            if (!paused)
            {
                StartCoroutine(Pause());
                paused = true;
            }
        }
    }

    public void ChangeNodeTarget()
    {
        //changing the target node to a different one - and one that is not the one the enemy is currently at
        // may edit this later to include the players last known location?
        target = nodes[Random.Range(0, nodes.Length)];
        if (susanrb.transform.position == target.transform.position)
        {
            ChangeNodeTarget();
        }
    }

    public IEnumerator Pause ()
    {
        yield return new WaitForSeconds(1f);
        ChangeNodeTarget();
        paused = false;
    }




}


//foreach (Transform deets in nodes)
//        {
//            //print(deets.position);
//        }
//print(nodes[Random.Range(0, nodes.Length)]); 

//susanrb.transform.position = (nodes[Random.Range(0,nodes.Length)]).position;


//// Move our position a step closer to the target.
//float step = speed * Time.deltaTime; // calculate distance to move
//transform.position = Vector3.MoveTowards(transform.position, target.position, step);

//// Check if the position of the cube and sphere are approximately equal.
//if (Vector3.Distance(transform.position, target.position) < 0.001f)
//{
//    // Swap the position of the cylinder.
//    target.position *= -1.0f;
//}



//    float maxRange = 5;
//    RaycastHit hit;

//    if(Vector3.Distance(transform.position, player.position) < maxRange )
//   {
//    if(Physics.Raycast(transform.position, (player.position - transform.position), out hit, maxRange))
//    {
//        if(hit.transform == player)
//        {
//            // In Range and i can see you!
//        }
//    }
//}
