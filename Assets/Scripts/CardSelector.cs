using System.Collections.Generic;
using UnityEngine;

public sealed class CardSelector : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> allUpgrades;

    private Dictionary<System.Type, int> upgradeLevels = new Dictionary<System.Type, int>();

    private void Start()
    {
        // ������������� ������� ��� ������� ���� ��������
        foreach (var upgrade in allUpgrades)
        {
            if (!upgradeLevels.ContainsKey(upgrade.GetType()))
            {
                upgradeLevels[upgrade.GetType()] = 1;
            }
        }
    }

    public List<UpgradeData> GetRandomUpgrades(int count)
    {
        List<UpgradeData> availableUpgrades = new List<UpgradeData>();

        // ������� ��� ��������� �������� ��� ������� �������
        foreach (var upgrade in allUpgrades)
        {
            var upgradeType = upgrade.GetType();
            if (upgradeLevels.ContainsKey(upgradeType) && upgrade.UpgradeLevel <= upgradeLevels[upgradeType])
            {
                availableUpgrades.Add(upgrade);
            }
        }

        // ��������� ����� �� ��������� ���������
        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        for (int i = 0; i < count && availableUpgrades.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            randomUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex); // ������� ��������� �������, ����� �������� ����������
        }

        return randomUpgrades;
    }

    public void UpgradeSelected(UpgradeData selectedUpgrade)
    {
        System.Type upgradeType = selectedUpgrade.GetType(); // �� ������ ������� �������������� �����

        // �������� ������� �������� ��� ���������� ����
        if (upgradeLevels.ContainsKey(upgradeType))
        {
            upgradeLevels[upgradeType] = selectedUpgrade.UpgradeLevel + 1;
        }
    }
}
