using UnityEngine;

[CreateAssetMenu(fileName = "BawlingUpgrade", menuName = "Upgrades/BawlingUpgrade")]
public class BawlingUpgrade : UpgradeData
{
    [SerializeField] private int _ballCount;

    public int BallCount => _ballCount;

    public override void ApplyUpgrade(GameObject target)
    {
        BowlingAttack shooter = target.GetComponent<BowlingAttack>();
        if (shooter != null)
        {
            if (shooter.enabled == false)
            {
                shooter.enabled = true;
            }

            shooter.ApplyChipShootingUpgrade(this);
        }
    }
}
