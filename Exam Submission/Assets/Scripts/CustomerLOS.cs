using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLOS : MonoBehaviour
{
    //reference for this detection was taken and adapted from the work of Sebastian Lague https://www.youtube.com/watch?v=rQG9aUWarwE 
    //and Ituozzo who did some 3D to 2D conversions of the original codes https://gist.github.com/ltuozzo/112138ad42c9530de15b5bc22226b739

    [Header("References")]
    public float viewRadius;
    public CustomerAI customer;
    [Range(0,360)]
    public float viewAngle;


    public LayerMask playermask;
    public LayerMask levelmask;

    public void Start()
    {
        StartCoroutine(FindPlayerDelayed(0.2f));
    }
    public void FindPlayer()
    {
        Collider2D playerping = Physics2D.OverlapCircle(transform.position, viewRadius, playermask);

        if (playerping != null)
        {
            if (playerping.gameObject.tag == "Player")
            {
                Vector3 dirToPlayer = (playerping.transform.position - transform.position).normalized;
                float dstToPlayer = Vector3.Distance(transform.position, playerping.transform.position);

                //draw ray from enemy to when the player came on their radar
                //checks if they are blocked by a shelf
                if (!Physics2D.Raycast(transform.position, dirToPlayer, dstToPlayer, levelmask ))
                {
                    //makes customer stop what they are doing and move half the distance towards player :)

                    Vector3 halfwaypoint = new Vector3((this.transform.position.x + playerping.transform.position.x) / 2, (this.transform.position.y + playerping.transform.position.y) / 2, 0);

                    //Vector3 newpos = new Vector3((-enemylastPos.x + 2*playerlastPos.x), (-enemylastPos.y + 2*playerlastPos.y), 0);



                    customer.ApproachPlayer(halfwaypoint);

                }
                
            }

        }

        



    }

    public IEnumerator FindPlayerDelayed (float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindPlayer();
        }
    }







    //method for converting angles to vector3 direction 
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
   

}
