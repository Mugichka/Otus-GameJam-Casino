using UnityEngine;

public sealed class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Chip _chipPrefab;
    [SerializeField] private int _chipPoolSize;
    [SerializeField] private Enemy _enemyNormalPrefab;
    [SerializeField] private int _enemyNormalPoolSize;
    [SerializeField] private Enemy _EnemyLightweightPrefab;
    [SerializeField] private int _EnemyLightweightPoolSize;
    [SerializeField] private Enemy _enemyHeavyweightPrefab;
    [SerializeField] private int _enemyHeavyweightPoolSize;
    [SerializeField] private Fire _firePrefab;
    [SerializeField] private int _firePoolSize;
    [SerializeField] private BawlingBall _bawlingBallPrefab;
    [SerializeField] private int _bawlingBallPoolSize;

    private ObjectPool<Chip> _chipPool;
    private ObjectPool<Enemy> _enemyNormalPool;
    private ObjectPool<Enemy> _enemyLightweightPool;
    private ObjectPool<Enemy> _enemyHeavyweightPool;
    private ObjectPool<Fire> _firePool;
    private ObjectPool<BawlingBall> _bawlingBallPool;

    public ObjectPool<Chip> ChipPool => _chipPool;
    public ObjectPool<Enemy> EnemyNormalPool => _enemyNormalPool;
    public ObjectPool<Enemy> EnemyLightweightPool => _enemyLightweightPool;
    public ObjectPool<Enemy> EnemyHeavyweightPool => _enemyHeavyweightPool;
    public ObjectPool<Fire> FirePool => _firePool;
    public ObjectPool<BawlingBall> BawlingBallPool => _bawlingBallPool;

    private void Awake()
    {
        _chipPool = new ObjectPool<Chip>(_chipPrefab, _chipPoolSize);
        _enemyNormalPool = new ObjectPool<Enemy>(_enemyNormalPrefab, _enemyNormalPoolSize);
        _enemyLightweightPool = new ObjectPool<Enemy>(_EnemyLightweightPrefab, _EnemyLightweightPoolSize);
        _enemyHeavyweightPool = new ObjectPool<Enemy>(_enemyHeavyweightPrefab, _enemyHeavyweightPoolSize);
        _firePool = new ObjectPool<Fire>(_firePrefab, _firePoolSize);
        _bawlingBallPool = new ObjectPool<BawlingBall>(_bawlingBallPrefab, _bawlingBallPoolSize);
    }
}
