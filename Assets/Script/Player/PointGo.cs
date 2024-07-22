using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointGo : MonoBehaviour
{
    [SerializeField] 
    public float speed = 10f;
    
    private Vector2 lastclickposition;
    public bool moving;

    [SerializeField]
    private Transform body;

    [SerializeField] private Player player;


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
            transform.position += (speed + player.SpeedModifier) * Time.deltaTime * (Vector3)direction.normalized;
            body.up = direction;
        }
        else
        {
            moving = false;
        }
    }
}
