using UnityEngine;
using System;

public sealed class FortuneWheel : MonoBehaviour
{
    public event Action CardsFell;

    [SerializeField] private SpinButton _spinButton;
    [SerializeField] private float _minSpeedValue = 1000f;
    [SerializeField] private float _maxSpeedValue = 2000f;
    [SerializeField] private float _deceleration = 200f;

    private float _currentSpeed;
    private bool _isSpinning = false;
    private readonly float _wheelOffset = 11.5f;

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
        if (_isSpinning)
        {
            transform.Rotate(0, 0, _currentSpeed * Time.deltaTime);
            _currentSpeed -= _deceleration * Time.deltaTime;

            if (_currentSpeed <= 0)
            {
                _currentSpeed = 0;
                _isSpinning = false;
                DetermineResult();
            }
        }
    }

    public void StartSpin()
    {
        if (!_isSpinning)
        {
            _currentSpeed = UnityEngine.Random.Range(_minSpeedValue, _maxSpeedValue);
            _isSpinning = true;
        }
    }

    private void DetermineResult()
    {
        float angle = (transform.eulerAngles.z + _wheelOffset) % 360;

        if ((angle >= 22.5f && angle < 45f) || (angle >= 90 && angle < 112.5f) || (angle >= 157.5f && angle < 180f) ||
         (angle >= 225f && angle < 247.5f) || (angle >= 292.5f && angle < 315f))
        {
            Debug.Log("Минус деньги");
        }
        else if ((angle >= 45f && angle < 67.5f) || (angle >= 112.5f && angle < 135f) || (angle >= 180 && angle < 202.5f) ||
                 (angle >= 247.5f && angle < 270f) || (angle >= 315f && angle < 337.5f))
        {
            CardsFell?.Invoke();
        }
        else if ((angle >= 67.5f && angle < 90f) || (angle >= 135f && angle < 157.5f) || (angle >= 202.5f && angle < 225f) ||
                  (angle >= 270 && angle < 292.5f) || (angle >= 337.5f && angle < 360f))
        {
            Debug.Log("Призвать противников");
        }
        else if (angle >= 0 && angle < 22.5f)
        {
            Debug.Log("Jackpot!");
        }
    }
}
