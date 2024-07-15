using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGo : MonoBehaviour
{
    public float speed = 10f;
    Vector2 lastclickposition;
    bool moving;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastclickposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            moving = true;  
        }

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
