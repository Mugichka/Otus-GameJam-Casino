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
        StartCoroutine(ShowWheelResult("Выпали карточки! Удача на твоей стороне!", Color.green));
    }

    private void ShowMoneyText()
    {
        StartCoroutine(ShowWheelResult("Вы потеряли половину своих денег! Вот досада!", Color.yellow));
    }

    private void ShowEnemyText()
    {
        StartCoroutine(ShowWheelResult("Враги наступают! Готовьте щит, милорд!", Color.red));
    }

    private IEnumerator ShowWheelResult(string text, Color color)
    {
        _text.color = color;
        _text.text = text;
        yield return new WaitForSeconds(_textTimeDelay);
        _text.text = string.Empty;
    }
}
