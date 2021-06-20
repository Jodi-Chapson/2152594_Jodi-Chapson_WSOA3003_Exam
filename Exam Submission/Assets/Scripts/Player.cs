using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("References")]
    public bool canmove;
    public Rigidbody2D rb;
    public float Speed;
    public float percentMod;


    void Start()
    {
        canmove = true;
    }

    
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (canmove)
        {
            if (Input.GetKey("d"))
            {
                rb.velocity = new Vector2(Speed, rb.velocity.y);
                //Flip();
            }
            else if (Input.GetKey("a"))
            {
                rb.velocity = new Vector2(-Speed, rb.velocity.y);

                //Flip();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }


            if (Input.GetKey("w"))
            {
                rb.velocity = new Vector2(rb.velocity.x, Speed * percentMod);
            }
            else if (Input.GetKey("s"))
            {
                rb.velocity = new Vector2(rb.velocity.x, -Speed * percentMod);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
}
