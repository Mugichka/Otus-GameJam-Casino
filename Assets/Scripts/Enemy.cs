using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyTrigger _enemyTrigger;
    [SerializeField] private int _damage;
    [SerializeField] private int _hP = 100;
    [SerializeField] private EnemyController _controller;

    private ObjectPoolManager _objectPoolManager;
    private int _currentHP;

    public EnemyController Controller => _controller;
    public int Damage => _damage;

    private void Awake()
    {
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
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

    private bool _isDead = false;

    public void TakeDamage(int damage)
    {
        if (_isDead == true)
        {
            return;
        }

        if (damage >= _currentHP)
        {
            _isDead = true;
            Die();
        }
        else
        {
            _currentHP -= damage;
        }
    }

    private void Die()
    {
        _objectPoolManager.EnemyPool.ReturnObjectToPool(this);
        UpdateEnemy();
    }

    private void UpdateEnemy()
    {
        _currentHP = _hP;
        _isDead = false;
    }
}
