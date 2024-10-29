using UnityEngine;
using System.Collections;

public class PillarOfFire : MonoBehaviour, IDelayBuffable
{
    [SerializeField] private Animator _fireAnimator;
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _fireCheckInterval;
    [SerializeField] private float _spawnRadius;

    public bool buffApplied =false;

    private FireUpgrade _currentUpgrade;

    private void Start()
    {
        StartCoroutine(FireDamageRoutine());
    }

    private IEnumerator FireDamageRoutine()
    {
        while (true)
        {
            for (int i = 0; i < _currentUpgrade.FireCount; i++)
            {
                Vector2 spawnPoint = GetRandomPointInsideCircle();
                Fire fire = _objectPoolManager.FirePool.GetObjectFromPool();
                StartCoroutine(fire.Run(spawnPoint));
            }

            yield return new WaitForSeconds(_fireCheckInterval);
        }
    }

    public Vector2 GetRandomPointInsideCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float randomRadius = Random.Range(0f, _spawnRadius);

        float xOffset = Mathf.Cos(angle) * randomRadius;
        float yOffset = Mathf.Sin(angle) * randomRadius;

        Vector2 randomPoint = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
        return randomPoint;
    }

    public void ApplyChipShootingUpgrade(FireUpgrade newUpgrade)
    {
        _currentUpgrade = newUpgrade;
    }

    public void ApplyDelayBuff(float buffAmount)
    {
        _fireCheckInterval *= buffAmount;
    }
}
