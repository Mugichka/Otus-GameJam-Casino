using UnityEngine;
using System;

public class SpinButtonTrigger : MonoBehaviour
{
    public event Action ButtonTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            ButtonTriggered?.Invoke();
        }
    }
}
