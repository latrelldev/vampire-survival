using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SpriteRenderer sprite;

    public void Update()
    {
        sprite.material.mainTextureOffset += Vector2.one * Time.deltaTime * 5;
    }

}
