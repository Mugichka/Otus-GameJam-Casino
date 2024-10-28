using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public event Action<int> UpdateMoney;

    [SerializeField] private int _money;
    [SerializeField] private PlayerTrigger _playerTrigger;

    private void Start()
    {
        UpdateMoney.Invoke(_money);
    }

    private void OnEnable()
    {
        _playerTrigger.PlayerDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        _playerTrigger.PlayerDamaged -= TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        _money -= damage;
        UpdateMoney.Invoke(_money);
    }
}
