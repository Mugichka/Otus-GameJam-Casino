using UnityEngine;

public sealed class MoneyTaker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FortuneWheel _fortuneWheel;

    private void OnEnable()
    {
        _fortuneWheel.MoneyFell += TakeMoney;
    }

    private void OnDisable()
    {
        _fortuneWheel.MoneyFell -= TakeMoney;
    }

    private void TakeMoney()
    {
        _player.Money /= 2;
    }
}
