using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointGo : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10f;
    
    private Vector2 lastclickposition;
    private bool moving;

    private void Start()
    {
        BackgroundClick.OnGroundClicked += OnGroundClicked;
    }

    private void OnDestroy()
    {
        BackgroundClick.OnGroundClicked -= OnGroundClicked;
    }

    private void OnGroundClicked(PointerEventData data)
    {
        lastclickposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moving = true;
    }

    private void Update()
    {
        if (moving && (Vector2)transform.position != lastclickposition) 
        {
            float step = speed * Time.deltaTime ;
            transform.position = Vector2.MoveTowards(transform.position,lastclickposition,step);
        }
        else
        {
            moving = false;
        }
    }
}
