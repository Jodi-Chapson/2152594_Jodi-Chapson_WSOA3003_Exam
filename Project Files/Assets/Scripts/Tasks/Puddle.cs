using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puddle : MonoBehaviour
{
   
    public RawImage puddlesprite;
    public float alpha = 1f;
    public MopTask task;

    
    void Start()
    {
        puddlesprite = this.GetComponent<RawImage>();
        alpha = 1f;
        //task.puddles++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sponge")
        {

            alpha -= 0.2f;
            Color currentcolor = puddlesprite.color;
            currentcolor.a = alpha;
            puddlesprite.color = currentcolor;


            if (alpha <= 0)
            {
                task.Cleaned();
                this.gameObject.SetActive(false);
            }



        }
    }
}
