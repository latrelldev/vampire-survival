using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovimentação : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rig;
    public Camera cam;
    public Vector2 movement;
    public Vector2 mousePosition;
 

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rig.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rig.rotation = angle;
    }

  

}
