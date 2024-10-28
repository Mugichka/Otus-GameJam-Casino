using UnityEngine;
using System;

public class PlayerTrigger : MonoBehaviour
{
    public event Action<int> PlayerDamaged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            PlayerDamaged?.Invoke(enemy.Damage);
        }
    }
}
