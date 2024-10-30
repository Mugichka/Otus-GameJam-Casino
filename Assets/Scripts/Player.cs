using UnityEngine;
using System;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public event Action PlayerDead;
    public event Action<int> UpdateMoney;
    public float damageCooldown = 0.5f;
    [SerializeField] private MoneyCounter _moneyCounter;

    // Dictionary to track last damage time for each enemy
    private Dictionary<Enemy, float> enemyDamageTimers = new Dictionary<Enemy, float>();

    [SerializeField] private int _money;

    public int Money 
    {
        get
        {
            return _money;
        } 
        set
        {
            _money = value;
            UpdateMoney.Invoke(_money);
            _moneyCounter.AddMoney(value);
        }
    }
    //[SerializeField] private PlayerTrigger _playerTrigger;

    private void Start()
    {
        UpdateMoney.Invoke(_money);
    }

    /*

    private void OnEnable()
    {
        //_playerTrigger.PlayerDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        //_playerTrigger.PlayerDamaged -= TakeDamage;
    }

    */

    private void TakeDamage(int damage)
    {
        _money -= damage;
        UpdateMoney.Invoke(_money);
     
        if (_money <= 0)
        {
            gameObject.SetActive(false);
            PlayerDead?.Invoke();
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            TryApplyDamage(enemy);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            TryApplyDamage(enemy);
        }
    }

    private void TryApplyDamage(Enemy enemy)
    {
        // Check if we have a record of this enemy in the dictionary
        if (!enemyDamageTimers.ContainsKey(enemy))
        {
            enemyDamageTimers[enemy] = -Mathf.Infinity; // Initialize with a value so it can take damage immediately
        }

        // Check if enough time has passed to take damage from this specific enemy
        if (Time.time >= enemyDamageTimers[enemy] + damageCooldown)
        {
            // Apply damage to the player
            TakeDamage(enemy.Damage);

            // Update the last damage time for this enemy
            enemyDamageTimers[enemy] = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            // Remove the enemy from the dictionary when it's no longer colliding
            enemyDamageTimers.Remove(enemy);
        }
    }
}
