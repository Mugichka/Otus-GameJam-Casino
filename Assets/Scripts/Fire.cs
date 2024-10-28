using UnityEngine;
using System.Collections;

public sealed class Fire : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private float _animationTime = 0.833f;
    private ObjectPoolManager _objectPoolManager;
    private Vector2 _fireOffset;
    private Collider2D[] _hitColliders = new Collider2D[64];
    private readonly int _damageTimes = 2;
    private readonly Vector2 _fireSize = new Vector2(0.8f, 2.4f);

    private void Awake()
    {
        _fireOffset = new Vector2(0, _fireSize.y / 2);
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
    }

    public IEnumerator Run(Vector2 spawnPoint)
    {
        transform.position = spawnPoint;
        _animator.SetTrigger("DoFire");

        for (int i = 0; i < _damageTimes; i++)
        {
            yield return new WaitForSeconds(_damageDelay);
            ApplyDamageToNearby(spawnPoint);
        }

        yield return new WaitForSeconds(_animationTime - _damageDelay - _damageDelay);

        Die();
    }

    private void ApplyDamageToNearby(Vector2 spawnPoint)
    {
        int hits = Physics2D.OverlapBoxNonAlloc(spawnPoint + _fireOffset, _fireSize, 0f, _hitColliders);

        for (int i = 0; i < hits; i++)
        {
            Collider2D collider = _hitColliders[i];

            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void Die()
    {
        _objectPoolManager.FirePool.ReturnObjectToPool(this);
    }
}
