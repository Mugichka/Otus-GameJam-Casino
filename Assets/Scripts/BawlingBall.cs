using UnityEngine;
using System.Collections;

public sealed class BawlingBall : MonoBehaviour,IDamageBuffable,ISpeedBuffable
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _ballSpeed;
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
        enemy.DisableWalking();
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

    public void ApplyDamageBuff(float buffAmount)
    {
        _damage += (int)buffAmount;
    }

    public void ApplySpeedBuff(float buffAmount)
    {
        _ballSpeed += buffAmount;
    }
}
