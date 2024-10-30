using UnityEngine;
using System.Collections;

public sealed class BowlingAttack : MonoBehaviour, IDelayBuffable
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _shotDelay;
    [SerializeField] private PlayerController _controller;
    public bool buffApplied=false;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _audioClip;

    private BawlingUpgrade _currentUpgrade;

    private IEnumerator Start()
    {
        while(true)
        {
            Shot();
            yield return new WaitForSeconds(_shotDelay);
        }
    }

    Vector2 CalculateDirection(Vector2 baseDirection, int ballIndex, int totalBalls)
    {
        if (totalBalls == 1) return baseDirection;

        if (totalBalls == 2) return Quaternion.Euler(0, 0, ballIndex * 180f) * baseDirection;

        if (totalBalls == 4) return Quaternion.Euler(0, 0, ballIndex * 90f) * baseDirection;

        return baseDirection;
    }

    private void Shot()
    {
        _source.PlayOneShot(_audioClip);
        for (int i = 0; i < _currentUpgrade.BallCount; i++)
        {
            Vector2 direction = CalculateDirection(_controller.LastDirection, i, _currentUpgrade.BallCount);
            BawlingBall bawlingBall = _objectPoolManager.BawlingBallPool.GetObjectFromPool();
            bawlingBall.Run(transform, direction);
        }
    }

    public void ApplyChipShootingUpgrade(BawlingUpgrade newUpgrade)
    {
        _currentUpgrade = newUpgrade;
    }

    public void ApplyDelayBuff(float buffAmount)
    {
        _shotDelay *= buffAmount;
    }
}
