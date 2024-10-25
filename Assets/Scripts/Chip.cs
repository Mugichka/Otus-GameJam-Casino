using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Chip : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _chipSpeed;
    [SerializeField] private int _damage;

    private ObjectPoolManager _objectPoolManager;
    private ChipTrigger _chipTrigger;
    private Rigidbody2D _rigidBody;

    public int Damage => _damage;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _chipTrigger = GetComponent<ChipTrigger>();
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
    }

    private void OnEnable()
    {
        _chipTrigger.ChipTriggered += Die;
        //StartCoroutine(Run());
    }

    private void OnDisable()
    {
        _chipTrigger.ChipTriggered -= Die;
    }

    private void Die()
    {
        _objectPoolManager.ChipPool.ReturnObjectToPool(this);
    }

    public IEnumerator Run(Transform playerTransform)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        transform.position = playerTransform.position;
        Vector3 playerVelocityAlongChipDirection = Vector3.Project(_rigidBody.velocity, direction);
        _rigidBody.velocity = direction * _chipSpeed + playerVelocityAlongChipDirection;

        yield return new WaitForSeconds(_lifeTime);
        Die();
    }
}
