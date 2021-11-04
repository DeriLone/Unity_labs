using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeInvisible : MonoBehaviour
{
    SpriteRenderer sprite;
    
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Color color = sprite.material.color;
        color.a = 0f;
        sprite.material.color = color;

        gameObject.SetActive(false);
    }
}
