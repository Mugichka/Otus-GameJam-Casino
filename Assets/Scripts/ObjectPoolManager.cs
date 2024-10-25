using UnityEngine;

public sealed class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Chip _chipPrefab;
    [SerializeField] private int _chipPoolSize;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyPoolSize;

    private ObjectPool<Chip> _chipPool;
    private ObjectPool<Enemy> _enemyPool;

    public ObjectPool<Chip> ChipPool => _chipPool;
    public ObjectPool<Enemy> EnemyPool => _enemyPool;

    private void Awake()
    {
        _chipPool = new ObjectPool<Chip>(_chipPrefab, _chipPoolSize);
        _enemyPool = new ObjectPool<Enemy>(_enemyPrefab, _enemyPoolSize);
    }
}
