using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Player player;

    void Update()
    {
        var pos = player.transform.position;
        pos.z = -10;
        transform.position = pos;
    }
}
