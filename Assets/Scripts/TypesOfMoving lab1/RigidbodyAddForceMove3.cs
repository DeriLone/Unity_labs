using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyAddForceMove3 : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    private Vector2 _direction;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        
        if (_direction.x > 0 && _spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * _speed);

    }

    
}
