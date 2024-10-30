using UnityEngine;
using TMPro;
using System.Collections;

public sealed class WheelResultShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private FortuneWheel _fortuneWheel;
    [SerializeField] private float _textTimeDelay;

    private void OnEnable()
    {
        _fortuneWheel.CardsFell += ShowCardText;
        _fortuneWheel.EnemyFell += ShowEnemyText;
        _fortuneWheel.MoneyFell += ShowMoneyText;
    }

    private void ShowCardText()
    {
        StartCoroutine(ShowWheelResult("������ ��������! ����� �� ����� �������!", Color.green));
    }

    private void ShowMoneyText()
    {
        StartCoroutine(ShowWheelResult("�� �������� �������� ����� �����! ��� ������!", Color.yellow));
    }

    private void ShowEnemyText()
    {
        StartCoroutine(ShowWheelResult("����� ���������! �������� ���, ������!", Color.red));
    }

    private IEnumerator ShowWheelResult(string text, Color color)
    {
        _text.color = color;
        _text.text = text;
        yield return new WaitForSeconds(_textTimeDelay);
        _text.text = string.Empty;
    }
}
