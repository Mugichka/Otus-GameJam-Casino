using UnityEngine;

public sealed class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Chip _chipPrefab;
    [SerializeField] private int _chipPoolSize;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyPoolSize;
    [SerializeField] private Fire _firePrefab;
    [SerializeField] private int _firePoolSize;

    private ObjectPool<Chip> _chipPool;
    private ObjectPool<Enemy> _enemyPool;
    private ObjectPool<Fire> _firePool;

    public ObjectPool<Chip> ChipPool => _chipPool;
    public ObjectPool<Enemy> EnemyPool => _enemyPool;
    public ObjectPool<Fire> FirePool => _firePool;

    private void Awake()
    {
        _chipPool = new ObjectPool<Chip>(_chipPrefab, _chipPoolSize);
        _enemyPool = new ObjectPool<Enemy>(_enemyPrefab, _enemyPoolSize);
        _firePool = new ObjectPool<Fire>(_firePrefab, _firePoolSize);
    }
}
