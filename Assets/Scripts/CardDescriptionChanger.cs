using UnityEngine;
using TMPro;

public class CardDescriptionChanger
{
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _description;
    private Sprite _sprite;

    public CardDescriptionChanger(TextMeshProUGUI name, TextMeshProUGUI description, GameObject player)
    {
        _name = name;
        _description = description;
    }

    public void SetDataFromUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData != null)
        {
            _name.text = upgradeData.UpgradeName;
            _description.text = upgradeData.Description;
            // Можно также установить спрайт, если есть
            // _sprite = upgradeData.sprite; // Если у вас есть спрайт в UpgradeData
        }
    }
}
