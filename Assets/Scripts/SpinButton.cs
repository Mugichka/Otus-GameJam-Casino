using UnityEngine;
using System;

public sealed class SpinButton : MonoBehaviour
{
    public event Action spinAllowed;

    [SerializeField] private SpinButtonTrigger _spinButtonTrigger;

    private void OnEnable()
    {
        _spinButtonTrigger.ButtonTriggered += DoSpin;
    }

    private void OnDisable()
    {
        _spinButtonTrigger.ButtonTriggered -= DoSpin;
    }

    private void DoSpin()
    {
        spinAllowed?.Invoke();
    }
}
