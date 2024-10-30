using UnityEngine;

[CreateAssetMenu(fileName = "totalMoney", menuName = "money")]
public sealed class MoneyCounter : ScriptableObject
{
    [SerializeField] private float _totalMoney;

    public float TotalMoney => _totalMoney;

    public void AddMoney(int money)
    {
        _totalMoney += money * 0.1f;
    }
}
