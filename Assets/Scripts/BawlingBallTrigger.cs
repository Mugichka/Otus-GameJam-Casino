using UnityEngine;
using System;

public sealed class BawlingBallTrigger : MonoBehaviour
{
    public event Action<Enemy> BawlingBallTriggered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            BawlingBallTriggered?.Invoke(enemy);
        }
    }
}
