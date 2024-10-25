using UnityEngine;
using System;

public sealed class EnemyTrigger : MonoBehaviour
{
    public event Action<int> EnemyTriggered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Chip chip))
        {
            EnemyTriggered?.Invoke(chip.Damage);
        }
    }
}
