using UnityEngine;

public class CameraMov : MonoBehaviour
{
    private Vector3 cameraPosition;
    [Header("camera settings")]
    public float cameraSpeed;


    private void Start()
    {
        cameraPosition = this.transform.position;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            cameraPosition.y -= cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraPosition.y += cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraPosition.x += cameraSpeed / 10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraPosition.x -= cameraSpeed / 10;
        }

        this.transform.position = cameraPosition;


    }
}
