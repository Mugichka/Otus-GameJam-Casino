using UnityEngine;
using TMPro;

public sealed class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float elapsedTime = 0f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        timerText.text = FormatTime(elapsedTime);
    }

    private string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
