using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    [SerializeField] private string _upgradeName;
    [SerializeField] private string _description;
    //[SerializeField] private string _nameOfUpgrade;
    [SerializeField] private int _upgradeLevel;

    public int UpgradeLevel => _upgradeLevel;
    public string UpgradeName => _upgradeName;
    public string Description => _description;
    //public bool IsUpgraded { get; set; } = false;

    public abstract void ApplyUpgrade(GameObject target);
}
