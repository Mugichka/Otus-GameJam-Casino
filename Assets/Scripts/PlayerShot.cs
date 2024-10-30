using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerShot : MonoBehaviour,IDelayBuffable
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _shotDelay;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _audioClip;

    public bool buffApplied=false;


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
        _source.PlayOneShot(_audioClip);
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

    public void ApplyDelayBuff(float buffAmount)
    {
        _shotDelay *= buffAmount;
    }
}
