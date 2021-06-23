using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SusanFOV : MonoBehaviour
{
    //reference for this detection was taken and adapted from the work of Sebastian Lague https://www.youtube.com/watch?v=rQG9aUWarwE 
    //and Ituozzo who did some 3D to 2D conversions of the original codes https://gist.github.com/ltuozzo/112138ad42c9530de15b5bc22226b739

    [Header("References")]
    public BasicEnemy susan;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    
    
    //rotates the field of view :D
    [Range(0,270)]
    public float anglemod;


    public LayerMask playermask;
    public LayerMask levelmask;

    public void Start()
    {
        StartCoroutine(FindPlayerDelayed(0.1f));
    }
    public void FindPlayer()
    {
        Collider2D playerping = Physics2D.OverlapCircle(transform.position, viewRadius, playermask);

        if (playerping != null)
        {
            if (playerping.gameObject.tag == "Player")
            {
                Vector3 dirToPlayer = (playerping.transform.position - transform.position).normalized;
                

                // needs to check whether the player is in the FOV angle :)

                Transform playertarget = playerping.transform;

                

                if (Vector3.Angle(transform.up, dirToPlayer) < viewAngle / 2)
                {
                    float dstToPlayer = Vector3.Distance(transform.position, playertarget.transform.position);

                    //draw ray from enemy to when the player came on their radar
                    //checks if they are blocked by a shelf
                    if (!Physics2D.Raycast(transform.position, dirToPlayer, dstToPlayer, levelmask))
                    {

                        Vector3 playercurrentpos = playertarget.position;
                        
                        susan.Track(playercurrentpos);


                    

                    }



                }

                

            }

        }





    }

    public IEnumerator FindPlayerDelayed(float delay)
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

        return new Vector3(Mathf.Sin((angleInDegrees + anglemod) * Mathf.Deg2Rad), Mathf.Cos((angleInDegrees + anglemod)* Mathf.Deg2Rad), 0);
    }
}
