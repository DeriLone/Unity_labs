using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class HellGato : MonoBehaviour, IEnemyInterface
{
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;
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

    private void Start()
    {
        _slider.maxValue = _maxHp;
        CurrentHp = _maxHp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y - 0.7f), new Vector2(_attackRange, 1));
    }
    
    private void FixedUpdate()
    {
        CheckForPlayer();
        _canvas.position = new Vector2(transform.position.x, _canvas.position.y);
    }

    private void CheckForPlayer()
    {
        Collider2D player = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.7f), new Vector2(_attackRange, 1), 0, _whatIsPlayer);
        if (player != null)
        {
            StartChase(player.transform.position);
        }
        
    }

    private void StartChase(Vector2 playerPosition)
    {
        if (transform.position.x > playerPosition.x && _faceRight ||
            transform.position.x < playerPosition.x && !_faceRight)
        {
            _faceRight = !_faceRight;
            transform.Rotate(0, 180, 0);
            _direction *= -1;
        }
        _rigidbody2D.velocity = Vector2.right * _direction * _speed;
    }

    private void OnCollisionStay2D(Collision2D other)
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
