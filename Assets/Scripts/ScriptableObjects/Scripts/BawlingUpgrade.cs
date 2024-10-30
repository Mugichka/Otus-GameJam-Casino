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

            foreach (var BawlingBall in FindObjectsOfType<BawlingBall>(true))
                {
                    if (!BawlingBall.buffApplied)
                   PlayerBuffs.Instance.ReapplyBuffsToAllSpells(BawlingBall.gameObject);
                    BawlingBall.buffApplied = true;
                }
                if(!shooter.buffApplied)
                {
                    PlayerBuffs.Instance.ReapplyBuffsToAllSpells(shooter.gameObject);
                    shooter.buffApplied = true;
                }

            shooter.ApplyChipShootingUpgrade(this);
        }
    }
}
