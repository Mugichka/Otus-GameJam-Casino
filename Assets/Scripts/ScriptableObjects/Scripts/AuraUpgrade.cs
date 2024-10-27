using UnityEngine;

[CreateAssetMenu(fileName = "AuraUpgrade", menuName = "Upgrades/AuraUpgrade"),]
public class AuraUpgrade : UpgradeData
{
    [SerializeField] private float _auraRadius;
    [SerializeField] private Vector2 _smokeScale;

    public float Radius => _auraRadius;
    public Vector2 SmokeScale => _smokeScale;

    public override void ApplyUpgrade(GameObject target)
    {
        DamageAura shooter = target.GetComponent<DamageAura>();
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
