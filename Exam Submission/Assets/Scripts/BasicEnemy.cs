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
    public float stall;
    public AIState state;
    public Transform target;
    public Transform[] nodes;
    public List<Transform> AvailableNodes = new List<Transform>();
    public LayerMask raymask;
    


    public void Start()
    {
        Scan();
        patrol = true;
        paused = false;
        state = AIState.PATROL;
    }

    public void Scan()
    {
        //cast a ray to all the nodes and keep track of which nodes are visible
        //puts all the values into the list 

        foreach (Transform nododo in nodes)
        {
            var direction = nododo.position - susanrb.transform.position;
            LayerMask raymask = LayerMask.GetMask("Level");

            // Vector3.Distance(nododo.position, susanrb.position
            RaycastHit2D hit = Physics2D.Raycast(susanrb.position, direction, Vector2.Distance(nododo.position, susanrb.position), raymask);
            
                if (hit.collider == null)
                {
                    AvailableNodes.Add(nododo);
                }
            
        }
        ChangeNodeTarget();
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ///temporary thing to trigger the changing of the target node
            //patrol = true;
            //ChangeNodeTarget();
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
        // may edit this later to include the players last known location


        target = AvailableNodes[Random.Range(0, AvailableNodes.Count)];

        if (susanrb.transform.position == target.transform.position)
        {
            ChangeNodeTarget();
        }
    }

    public IEnumerator Pause ()
    {
        //where she stops and contemplates life for a moment
        
        
        yield return new WaitForSeconds(stall);
        AvailableNodes.Clear();
        
        
        Scan();
        paused = false;
    }




}



