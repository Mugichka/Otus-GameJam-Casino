using System.Collections.Generic;
using UnityEngine;

public sealed class CardSelector : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> _allUpgrades;

    private int _currentMinLevel = 1;
    private int _countCards = 3; 

    private List<UpgradeData> GetUpgrades()
    {
        if (_allUpgrades.Count < _countCards)
        {
            _countCards = _allUpgrades.Count;
        }

        List<UpgradeData> selectedUpgrades = new List<UpgradeData>();

        while (selectedUpgrades.Count < _countCards && _allUpgrades.Count > 0)
        {
            List<UpgradeData> upgradesOfCurrentLevel = new List<UpgradeData>();

            foreach (UpgradeData upgrade in _allUpgrades)
            {
                if (upgrade.Level == _currentMinLevel)
                {
                    upgradesOfCurrentLevel.Add(upgrade);
                }
            }

            if (upgradesOfCurrentLevel.Count >= _countCards)
            {
                for (int i = 0; i < _countCards; i++)
                {
                    int index = Random.Range(0, upgradesOfCurrentLevel.Count);
                    selectedUpgrades.Add(upgradesOfCurrentLevel[index]);
                    upgradesOfCurrentLevel.RemoveAt(index);
                }
            }
            else
            {
                selectedUpgrades.AddRange(upgradesOfCurrentLevel);
                _currentMinLevel++;

                if (selectedUpgrades.Count >= _countCards)
                {
                    selectedUpgrades = selectedUpgrades.GetRange(0, _countCards);
                }
            }
        }

        _currentMinLevel = 1;
        return selectedUpgrades;
    }
}
