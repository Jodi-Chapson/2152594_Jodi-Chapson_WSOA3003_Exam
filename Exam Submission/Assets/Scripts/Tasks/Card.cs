using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler
{
    public Transform pos1, pos2;
    public Canvas _canvas;

    public bool summoning;
    public bool desummoning;
    public float lerp;


    public void Awake()
    {
        this.transform.position = pos1.position;
        _canvas = GetComponentInParent<Canvas>();

        summoning = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, _canvas.worldCamera, out pos);
        transform.position = _canvas.transform.TransformPoint(pos);
    }


    public void Update()
    {
        if (summoning)
        {
            
            
                //this.transform.position = Vector3.MoveTowards(this.transform.position, pos2.transform.position, speed * Time.deltaTime);
                float xvalue = Mathf.Lerp(this.transform.position.x, pos2.transform.position.x, lerp * Time.deltaTime);
                this.transform.position = new Vector3(xvalue, this.transform.position.y, this.transform.position.z);
            
            
        }

        if (desummoning)
        {
            float xvalue = Mathf.Lerp(this.transform.position.x, pos1.transform.position.x, lerp * Time.deltaTime);
            this.transform.position = new Vector3(xvalue, this.transform.position.y, this.transform.position.z);
        }

        



    }

    public void Summon (GameObject button)
    {
        button.SetActive(false);
        summoning = true;
        this.gameObject.SetActive(true);
        StartCoroutine(ToggleBool(0));

    }


    public void DeSummon()
    {
        desummoning = true;
        StartCoroutine(ToggleBool(1));
    }

    public IEnumerator ToggleBool(int type)
    {
        yield return new WaitForSeconds(1.5f);

        if (type == 0)
        {
            summoning = false;
        }
        else if (type == 1)
        {
            desummoning = false;
        }
    }



}
