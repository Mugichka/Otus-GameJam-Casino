using UnityEngine;

[CreateAssetMenu(fileName = "ChipUpgrade", menuName = "Upgrades/ChipUpgrade"),]
public class ChipUpgrade : ScriptableObject
{
    [SerializeField] private int _chipCount = 1;
    [SerializeField] private float _angleBetweenChips = 0f;

    public int ChipCount => _chipCount;
    public float AngleBetweenChips => _angleBetweenChips;
}
