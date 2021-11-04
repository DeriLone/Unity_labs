using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawnBecomeInvisible : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    IEnumerator VisibleSprite()
    {
        yield return new WaitForSeconds(1.5f);
        
        for (float f = 1f; f >= -0.05; f -= 0.05f)
        {
            Color color = _spriteRenderer.material.color;
            color.a = f;
            _spriteRenderer.material.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        StartCoroutine("VisibleSprite");
    }
}
