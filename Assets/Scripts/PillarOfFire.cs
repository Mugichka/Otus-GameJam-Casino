using UnityEngine;
using System.Collections;

public class PillarOfFire : MonoBehaviour
{
    //[SerializeField] private Animator _fireAnimator;
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private int _fireDamage;
    [SerializeField] private float _fireCheckInterval;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _damageDelay;

    //private FireUpgrade _currentUpgrade;
    private Collider2D[] _hitColliders = new Collider2D[128];
    private Vector2 _fireOffset;
    private readonly Vector2 _fireSize = new Vector2(0.8f, 2.4f);
    private readonly float _firstDamageDelay = 0.3f;

    private void Awake()
    {
        _fireOffset = new Vector2(0, _fireSize.y / 2);
    }

    private void Start()
    {
        StartCoroutine(FireDamageRoutine());
    }

    private IEnumerator FireDamageRoutine()
    {
        while (true)
        {
            Vector2 spawnPoint = GetRandomPointInsideCircle();
            SpawnFire(spawnPoint);
            yield return new WaitForSeconds(_firstDamageDelay);
            ApplyDamageToNearby(spawnPoint);
            yield return new WaitForSeconds(_damageDelay);
            ApplyDamageToNearby(spawnPoint);

            yield return new WaitForSeconds(_fireCheckInterval - _firstDamageDelay);
        }
    }

    private void SpawnFire(Vector2 spawnPoint)
    {
        Fire fire = _objectPoolManager.FirePool.GetObjectFromPool();
        fire.gameObject.transform.position = spawnPoint;
        fire.GetComponent<Animator>().SetTrigger("DoFire");
    }

    private void ApplyDamageToNearby(Vector2 spawnPoint)
    {
        int hits = Physics2D.OverlapBoxNonAlloc(spawnPoint + _fireOffset, _fireSize, 0f, _hitColliders);

        for (int i = 0; i < hits; i++)
        {
            Collider2D collider = _hitColliders[i];

            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_fireDamage);
            }
        }
    }

    public Vector2 GetRandomPointInsideCircle()
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float randomRadius = Random.Range(0f, _spawnRadius); // Случайное расстояние до центра

        float xOffset = Mathf.Cos(angle) * randomRadius;
        float yOffset = Mathf.Sin(angle) * randomRadius;

        Vector2 randomPoint = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
        return randomPoint;
    }

    //void OnDrawGizmosSelected()
    //{

    //    // Визуализация области столпа огня
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position + new Vector3(_fireOffset.x, _fireOffset.y), new Vector2(0.8f, 2.4f));
    //}
}
