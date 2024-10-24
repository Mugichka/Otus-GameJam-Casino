using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheel : MonoBehaviour
{
    public float startSpeed = 1000f;
    public float deceleration = 5f;
    private float currentSpeed;
    private bool isSpinning = false;

    private void Start()
    {
        StartSpin();
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
        Debug.Log("Остановилось на угле: " + currentAngle);
    }
}
