using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEye : MonoBehaviour
{
    [SerializeField] private float _seeRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private bool _faceRight;
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _pushPower;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationKey;
    
    private Vector2 _startPosition;
    private Vector3 _positionBeforeAttack;
    private int _direction = 1;
    private bool _readyToAttack = false;
    private bool _attack = false;
    private bool _endAttack = false;
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector2(_drawPosition.x, _drawPosition.y - 1), new Vector3(_seeRange * 2, 3, 0));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 1), new Vector3(_attackRange * 2, 3, 0));
    }

    private void Start()
    {
        _startPosition = transform.position;
    }
    
    private void Update()
    {
        if (_attack)
        {
            float step =  10f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, step);
            return;
        }

        if (_endAttack)
        {
            if (transform.position == _positionBeforeAttack)
            {
                _endAttack = false;
                return;
            }

            float step =  10f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _positionBeforeAttack, step);
        }


        if (_faceRight && transform.position.x > _startPosition.x + _seeRange)
        {
            Flip();
        }
        else if (!_faceRight && transform.position.x < _startPosition.x - _seeRange)
        {
            Flip();
        }
    }
    
    private void FixedUpdate()
    {
        if (_readyToAttack)
        {
            _rigidbody2D.velocity = Vector2.zero;
            return;
        }
        _rigidbody2D.velocity = Vector2.right * _direction * _speed;
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Collider2D playerSee = Physics2D.OverlapBox(new Vector2(_drawPosition.x, _drawPosition.y - 1), new Vector2(_seeRange * 2, 3), 0, _whatIsPlayer);
        Collider2D playerAttack = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 1), new Vector3(_attackRange * 2, 3, 0), 0, _whatIsPlayer);

        if (playerAttack != null)
        {
            _readyToAttack = true;
            _animator.SetBool(_attackAnimationKey, true);
            return;
        }
        else
        {
            _readyToAttack = false;
        }

        if (playerSee != null)
        {
            StartChase(playerSee.transform.position);
        }
    }

    private void StartChase(Vector2 playerPosition)
    {
        if (transform.position.x > playerPosition.x && _faceRight ||
            transform.position.x < playerPosition.x && !_faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
        _direction *= -1;
    }

    private void StartAttack()
    {
        _positionBeforeAttack = transform.position;
        _attack = true;
    }
    
    private void EndAttack()
    {
        _animator.SetBool(_attackAnimationKey, false);
        _attack = false;
        _endAttack = true;
        Invoke(nameof(CheckForPlayer), _attackDelay);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMover player = other.collider.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.TakeDamage(_damage, _pushPower, transform.position.x);   
        }
    }
}
