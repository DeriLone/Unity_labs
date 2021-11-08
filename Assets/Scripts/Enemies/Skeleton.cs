using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Skeleton : MonoBehaviour, IEnemyInterface
{
    [SerializeField] private float _walkRange;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _speed;
    [SerializeField] private bool _faceRight;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _pushPower;
    
    [SerializeField] private int _maxHp;
    [SerializeField] private GameObject _enemySystem;
    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _canvas;

    private int _currentHp;
    
    private Vector2 _startPosition;
    private int _direction = 1;
    private float _lastAttackTime;
    
    private int CurrentHp
    {
        get => _currentHp;
        set
        {
            _currentHp = value;
            _slider.value = value;
        }
    }
    private Vector2 _drawPosition
    {
        get
        {
            if (_startPosition == Vector2.zero)
                return transform.position;
            else
                return _startPosition;
        }
    }

    private void Start()
    {
        _startPosition = transform.position;
        _slider.maxValue = _maxHp;
        CurrentHp = _maxHp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(_drawPosition, new Vector3(_walkRange * 2, 1, 0));
    }
    
    private void Update()
    {
        if (_faceRight && transform.position.x > _startPosition.x + _walkRange)
        {
            Flip();
        }
        else if (!_faceRight && transform.position.x < _startPosition.x - _walkRange)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.right * _direction * _speed;
        _canvas.position = new Vector2(transform.position.x, _canvas.position.y);
    }
    
    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
        _direction *= -1;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMover player = other.collider.GetComponent<PlayerMover>();
        if (player != null && Time.time - _lastAttackTime > _attackDelay)
        {
            _lastAttackTime = Time.time;
            player.TakeDamage(_damage, _pushPower, transform.position.x);   
        }
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHp -= damage;
        if (CurrentHp <= 0)
        {
            Destroy(_enemySystem);
        }
    }
}
