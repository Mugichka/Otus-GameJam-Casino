using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    [SerializeField] EnemyTrigger _enemyTrigger;
    [SerializeField] int _hP = 100;

    private GameObject _enemyPool;
    private int _currentHP;

    private void Awake()
    {
        _enemyPool = GameObject.FindWithTag(MyConst.EnemyPool);
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

    private void TakeDamage(int damage)
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
        _enemyPool.GetComponent<ObjectPool>().ReturnObjectToPool(gameObject);
        UpdateEnemy();
    }

    private void UpdateEnemy()
    {
        _currentHP = _hP;
        _isDead = false;
    }
}
