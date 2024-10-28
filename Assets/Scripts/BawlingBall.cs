using UnityEngine;
using System.Collections;

public sealed class BawlingBall : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _enemyDropedTime;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BawlingBallTrigger _bawlingBallTrigger;

    private ObjectPoolManager _objectPoolManager;

    private void Awake()
    {
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
    }

    private void OnEnable()
    {
        _bawlingBallTrigger.BawlingBallTriggered += DropEnemy;
    }

    private void OnDisable()
    {
        _bawlingBallTrigger.BawlingBallTriggered -= DropEnemy;
    }

    public void Run(Transform playerTransform, Vector2 playerDirection)
    {
        transform.position = playerTransform.position;
        _rigidbody.velocity = playerDirection * _ballSpeed;
        _rigidbody.angularVelocity = _rotationSpeed;

        StartCoroutine(RunLifetime());
    }

    private void DropEnemy(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
        EnemyController enemyController = enemy.Controller;
        enemyController.enabled = false;
        enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
        StartCoroutine(RaiseEnemy(enemy));
    }

    private IEnumerator RaiseEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(_enemyDropedTime);

        enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
        enemy.Controller.enabled = true;
    }

    private IEnumerator RunLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Die();
    }

    private void Die()
    {
        _objectPoolManager.BawlingBallPool.ReturnObjectToPool(this);
    }
}
