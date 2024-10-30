using UnityEngine;

[CreateAssetMenu(fileName = "ChipUpgrade", menuName = "Upgrades/ChipUpgrade")]
public class ChipUpgrade : UpgradeData
{
    [SerializeField] private int _chipCount;
    [SerializeField] private float _angleBetweenChips;

    public int ChipCount => _chipCount;
    public float AngleBetweenChips => _angleBetweenChips;

    public override void ApplyUpgrade(GameObject target)
    {
        PlayerShot shooter = target.GetComponent<PlayerShot>();
        if (shooter != null)
        {
            if (shooter.enabled == false)
            {
                shooter.enabled = true;
            }

            shooter.ApplyChipShootingUpgrade(this);
            // Apply any active buffs to all spells, including newly enabled ones
            //PlayerBuffs.Instance.ReapplyBuffsToAllSpells(shooter.gameObject);
            
                foreach (var chip in FindObjectsOfType<Chip>(true))
                {
                    if (!chip.buffApplied)
                   PlayerBuffs.Instance.ReapplyBuffsToAllSpells(chip.gameObject);
                    chip.buffApplied = true;
                }
                if(!shooter.buffApplied)
                {
                    PlayerBuffs.Instance.ReapplyBuffsToAllSpells(shooter.gameObject);
                    shooter.buffApplied = true;
                }
            

            

        }
    }
}
