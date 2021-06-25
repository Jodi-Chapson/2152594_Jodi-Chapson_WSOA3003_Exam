using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayScript : MonoBehaviour
{
    public GameObject item1, item2, item3;
    
    public bool onescanned, twoscanned, threescanned;
    public bool onescanning, twoscanning, threescanning;
    public float scanamount1, scanamount2, scanamount3;
    public RawImage ray;

    public float scantime = 3f;
    public TillTask till;



    void Start()
    {
        ray = this.GetComponent<RawImage>();
        ray.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        



        if (onescanning && !onescanned)
        {
            scanamount1 += Time.deltaTime;
            if (scanamount1 > scantime)
            {
                onescanned = true;

                StartCoroutine(Blink());

                if (onescanned && twoscanned && threescanned)
                {
                    Bingo();
                }
            }
        }

        if (twoscanning && !twoscanned)
        {
            scanamount2 += Time.deltaTime;
            if (scanamount2 > scantime)
            {
                twoscanned = true;

                StartCoroutine(Blink());

                if (onescanned && twoscanned && threescanned)
                {
                    Bingo();
                }
            }
        }


        if (threescanning && !threescanned)
        {
            scanamount3 += Time.deltaTime;
            if (scanamount3 > scantime)
            {
                threescanned = true;

                StartCoroutine(Blink());

                if (onescanned && twoscanned && threescanned)
                {
                    Bingo();
                }
            }
        }


    }

    public IEnumerator Blink()

    {

        ray.color = Color.green;


        yield return new WaitForSeconds(0.5f);

        ray.color = Color.red;




    }
    public void Bingo()
{
        till.Completed();
}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sponge")
        {

            Debug.Log(collision.gameObject);

            if (collision.gameObject == item1)
            {
                onescanning = true;
            }

            if (collision.gameObject == item2)
            {
                twoscanning = true;
            }

            if (collision.gameObject == item3)
            {
                threescanning = true;
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sponge")
        {
            if (collision == item1)
            {
                onescanning = false;
                scanamount1 = 0;
            }

            if (collision == item2)
            {
                twoscanning = false;
                scanamount2 = 0;
            }

            if (collision == item3)
            {
                threescanning = false;
                scanamount3 = 0;
            }


        }
    }

}
