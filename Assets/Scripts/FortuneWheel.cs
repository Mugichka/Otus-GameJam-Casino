using UnityEngine;
using System;

public sealed class FortuneWheel : MonoBehaviour
{
    public event Action CardsFell;

    [SerializeField] private SpinButton _spinButton;
    [SerializeField] private float startSpeed = 1000f;
    [SerializeField] private float deceleration = 200f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private bool isSpinning = false;

    private void OnEnable()
    {
        _spinButton.spinAllowed += StartSpin;
    }

    private void OnDisable()
    {
        _spinButton.spinAllowed -= StartSpin;
    }

    void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);
            currentSpeed -= deceleration * Time.deltaTime;

            if (currentSpeed <= 0)
            {
                currentSpeed = 0;
                isSpinning = false;
                DetermineResult();
            }
        }
    }

    public void StartSpin()
    {
        if (!isSpinning)
        {
            currentSpeed = startSpeed;
            isSpinning = true;
        }
    }

    private void DetermineResult()
    {
        float currentAngle = transform.eulerAngles.z;

        CardsFell?.Invoke();
    }
}
