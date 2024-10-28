using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardDescriptionChanger
{
    private TextMeshProUGUI _name;
    private TextMeshProUGUI _description;
    private Image _image;

    public CardDescriptionChanger(TextMeshProUGUI name, TextMeshProUGUI description, Image image ,GameObject player)
    {
        _name = name;
        _description = description;
        _image = image;
    }

    public void SetDataFromUpgrade(UpgradeData upgradeData)
    {
        if (upgradeData != null)
        {
            _name.text = upgradeData.UpgradeName;
            _description.text = upgradeData.Description;
            _image.sprite = upgradeData.Sprite;
        }
    }
}
