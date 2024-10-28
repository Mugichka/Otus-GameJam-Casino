using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerShot : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _shotDelay;

    private ChipUpgrade _currentUpgrade;

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(_shotDelay);
            Shot();
        }
    }

    private void Shot()
    {

        for (int i = 0; i < _currentUpgrade.ChipCount; i++)
        {
            float angleOffset = (i - (_currentUpgrade.ChipCount - 1) / 2f) * _currentUpgrade.AngleBetweenChips;
            Chip chip = _objectPoolManager.ChipPool.GetObjectFromPool();
            chip.Run(transform, angleOffset);
        }
    }

    public void ApplyChipShootingUpgrade(ChipUpgrade newUpgrade)
    {
        _currentUpgrade = newUpgrade;
    }
}
