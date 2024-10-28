using UnityEngine;
using TMPro;

public sealed class MoneyIndicator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _money;

    private void OnEnable()
    {
        _player.UpdateMoney += ShowMoneyCount;
    }

    private void OnDisable()
    {
        _player.UpdateMoney -= ShowMoneyCount;
    }

    private void ShowMoneyCount(int money)
    {
        _money.text = $"{money}";
    }
}
