using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTranslateMove2 : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    private Vector2 _direction;

    private void Start()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(_direction * _speed * Time.deltaTime);
        
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        
       
    }
}
