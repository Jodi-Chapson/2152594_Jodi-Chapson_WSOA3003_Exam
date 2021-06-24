using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Beans : MonoBehaviour,  IDragHandler
{
    public Canvas _canvas;

    public void Awake()
    {
        
        _canvas = GetComponentInParent<Canvas>();

        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, _canvas.worldCamera, out pos);
        transform.position = _canvas.transform.TransformPoint(pos);
    }
}
