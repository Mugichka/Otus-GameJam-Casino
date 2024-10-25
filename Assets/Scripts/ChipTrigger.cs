using UnityEngine;
using System;

public sealed class ChipTrigger : MonoBehaviour
{
    public event Action ChipTriggered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChipTriggered?.Invoke();
    }
}
