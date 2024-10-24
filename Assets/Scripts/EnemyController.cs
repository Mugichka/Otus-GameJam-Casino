using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;

    private PlayerController _target;
    private Rigidbody2D _rigidBody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        _direction = (_target.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _direction * _speed;
    }
}