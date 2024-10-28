using UnityEngine;

[CreateAssetMenu(fileName = "FireUpgrade", menuName = "Upgrades/FireUpgrade"),]
public class FireUpgrade : UpgradeData
{
    [SerializeField] private int _fireCount;

    public int FireCount => _fireCount;

    public override void ApplyUpgrade(GameObject target)
    {
        PillarOfFire shooter = target.GetComponent<PillarOfFire>();
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
