using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPositionMove1 : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    private Vector3 _direction;
    
    private void Start()
    {
        _transform = GetComponent<Transform>();
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _direction = new Vector2(-_speed, 0); 
            _transform.position += (_direction * Time.deltaTime);
            _spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _direction = new Vector2(_speed, 0); 
            _transform.position += (_direction * Time.deltaTime);
            _spriteRenderer.flipX = false;
        }
    }
}
