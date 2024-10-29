using UnityEngine;
using System.Collections;

public sealed class BowlingAttack : MonoBehaviour,IDelayBuffable
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _shotDelay;
    [SerializeField] private PlayerController _controller;

    private ChipUpgrade _currentUpgrade;

    private IEnumerator Start()
    {
        while(true)
        {
            Shot();
            yield return new WaitForSeconds(_shotDelay);
        }
    }

    private void Shot()
    {
        BawlingBall bawlingBall = _objectPoolManager.BawlingBallPool.GetObjectFromPool();
        bawlingBall.Run(transform, _controller.LastDirection);
    }

    public void ApplyDelayBuff(float buffAmount)
    {
        _shotDelay *= buffAmount;
    }
}
