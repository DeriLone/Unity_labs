using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecomeVisible : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    IEnumerator VisibleSprite()
    {
        for (float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color color = _spriteRenderer.material.color;
            color.a = f;
            _spriteRenderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartVisible()
    {
        StartCoroutine("VisibleSprite");
    }
    
    
}
