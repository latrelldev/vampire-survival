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
    public bool moving;

    [SerializeField]
    private Transform body;


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
            var direction = lastclickposition - (Vector2)transform.position;
            transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;

            body.up = direction;
            //Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //rb.MovePosition(Time.deltaTime * speed * direction.normalized);
            //transform.position = Vector2.MoveTowards(transform.position,lastclickposition,step);
        }
        else
        {
            moving = false;
        }
    }
}
