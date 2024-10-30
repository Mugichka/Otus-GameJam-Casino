using UnityEngine;
using System.Collections;
using System;

public sealed class Enemy : MonoBehaviour
{
    //public event Action<float> EnemyDead;

    [SerializeField] private EnemyTrigger _enemyTrigger;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage;
    [SerializeField] private int _hP = 100;
    [SerializeField] private float _dropedTime;
    [SerializeField] private EnemyController _controller;
    [SerializeField] private int _price;

    private Player _player;
    private SpriteRenderer _spriteRenderer;
    private ObjectPoolManager _objectPoolManager;
    private Color _originalColor;
    private int _currentHP;
    private bool _isDead = false;
    
    public int Damage => _damage;

    private void Awake()
    {
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _player = FindObjectOfType<Player>();
        _originalColor = _spriteRenderer.color;
        _currentHP = _hP;
    }

    private void OnEnable()
    {
        _enemyTrigger.EnemyTriggered += TakeDamage;
    }

    private void OnDisable()
    {
        _enemyTrigger.EnemyTriggered -= TakeDamage;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead == true)
        {
            return;
        }

        if (damage >= _currentHP)
        {
            _isDead = true;
            _player.Money += _price;
            //EnemyDead(_price);
            Die();
        }
        else
        {
            _currentHP -= damage;
            _animator.SetTrigger("TakeDamage");
        }
    }

    public void DisableWalking()
    {
        _controller.IsWalking = false;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        Invoke(nameof(EnableWalking), _dropedTime);
    }

    private void EnableWalking()
    {
        _controller.IsWalking = true;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Die()
    {
        _objectPoolManager.EnemyNormalPool.ReturnObjectToPool(this);
        UpdateEnemy();
    }

    private void UpdateEnemy()
    {
        _currentHP = _hP;
        _isDead = false;
        _spriteRenderer.color = _originalColor;
    }
}
