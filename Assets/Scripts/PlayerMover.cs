using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckerRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Collider2D _headColider;
    [SerializeField] private float _headCheckerRadius;
    [SerializeField] private Transform _headChecker;

    [Header("Animation")] 
    [SerializeField] private Animator _animator;
    [SerializeField] private string _runAnimatorKey;
    [SerializeField] private string _jumpAnimatorKey;
    [SerializeField] private string _crouchAnimatorKey;
    
    private float _direction;
    private bool _jump;
    private bool _crawl;
    private bool _isAttacking = false;
    private bool _getHurt = false;

    [Header("Stats")] 
    [SerializeField] private int _MaxHp;
    [SerializeField] private int _MaxStamina;
    
    private int _currentHp;
    private int _currentStamina;
    private bool activeStaminaRestore = false;

    [Header("UI")] 
    [SerializeField] private TMP_Text _coinsAmountText;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TMP_Text _hpTextBar;
    [SerializeField] private TMP_Text _hpToRestore;
    [SerializeField] private Slider _staminaBar;

    private int _coinsAmount;

    public int CoinsAmount
    {
        get  => _coinsAmount;
        set
        {
            _coinsAmount = value;
            _coinsAmountText.text = value.ToString();
        }
    }
    
    private int CurrentHp
    {
        get  => _currentHp;
        set
        {
            if (value > _MaxHp)
                value = _MaxHp;
            _currentHp = value;
            _hpBar.value = value;
            _hpTextBar.text = value.ToString();
        }
    }

    private int CurrentStamina
    {
        get => _currentStamina;
        set
        {
            if (value > _MaxStamina)
                value = _MaxStamina;
            _currentStamina = value;
            _staminaBar.value = value;
        }
    }

    private void Start()
    {
        _coinsAmount = 0;
        _coinsAmountText.text = _coinsAmount.ToString();
        
        _hpBar.maxValue = _MaxHp;
        CurrentHp = _MaxHp;
        _hpTextBar.text = _MaxHp.ToString();
        _hpToRestore.text = "";

        _staminaBar.maxValue = _MaxStamina;
        CurrentStamina = _MaxStamina;
        
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        _animator.SetFloat(_runAnimatorKey, Mathf.Abs(_direction));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jump = true;
        } 
        
        if (_direction > 0 && _spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction < 0 && !_spriteRenderer.flipX)
        {
            _spriteRenderer.flipX = true;
        }

        _crawl = Input.GetKey(KeyCode.C);
 
        if (Input.GetButtonDown("Fire1") && !_isAttacking && CurrentStamina >= 10)
        {
            CurrentStamina -= 10;
            _isAttacking = true;
            
            _animator.Play("attack");
        }
        else
        {
            _isAttacking = false;
        }

        if (_getHurt)
        {
            _getHurt = false;
        }
        
        if (CurrentStamina != _MaxStamina && !activeStaminaRestore)
        {
            activeStaminaRestore = true;
            StartCoroutine(AutoRestoreStamina());
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * _speed, _rigidbody.velocity.y);

        bool canJump = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround);
        bool canStand = !Physics2D.OverlapCircle(_headChecker.position, _headCheckerRadius, _whatIsGround);

        
        _headColider.enabled = !_crawl && canStand;
        if (_jump && canJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _jump = false;
        }

        
        _animator.SetBool(_jumpAnimatorKey, !canJump);
        _animator.SetBool(_crouchAnimatorKey, !_headColider.enabled);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckerRadius);
        Gizmos.color = Color.red;  
        Gizmos.DrawWireSphere(_headChecker.position, _headCheckerRadius);

    }

    public void AddHp(int hpPoints, float regenerationRate)
    {
        _hpToRestore.text = "+" + hpPoints;
        int missingHp = _MaxHp - CurrentHp;
        int hpPointsToAdd = missingHp > hpPoints ? hpPoints : missingHp;
        StartCoroutine(RestoreHp(hpPointsToAdd, regenerationRate));
    }

    private IEnumerator RestoreHp(int hpPointsToAdd, float regenerationRate)
    {
        while (hpPointsToAdd != 0)
        {
            hpPointsToAdd--;
            CurrentHp++;
            yield return new WaitForSeconds(regenerationRate);
        }

        _hpToRestore.text = "";
    }
    
    public void AddBuff(string buff)
    {
        Debug.Log("You are inspired\nYour max " + buff + " is increased");
        if (buff == "HP")
        {
            _MaxHp += 10;
            _hpBar.maxValue = _MaxHp;

            CurrentHp += 10;
        }
    }

    public void AddStamina(int staminaPoints)
    {
        CurrentStamina += staminaPoints;
    }
    public int GetMaxHp()
    {
        return _MaxHp;
    }
    
    public void TakeDamage(int damage)
    {
        if (!_getHurt)
        {
            _getHurt = true;
            _animator.Play("hurt");

            CurrentHp -= damage;
            
            if (CurrentHp <= 0)
            {
                Debug.Log("You are dead");
                gameObject.SetActive(false);
                Invoke("ReloadScene", 1f);
            }
        }
    }

    private IEnumerator AutoRestoreStamina()
    {
        while (CurrentStamina != _MaxStamina)
        {
            CurrentStamina++;
            yield return new WaitForSeconds(0.6f);
        }

        activeStaminaRestore = false;
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
