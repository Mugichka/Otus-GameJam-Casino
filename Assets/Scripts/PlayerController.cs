using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed; 
 
    private Rigidbody2D _rigidBody;
    private Vector2 _direction;
    private Vector2 _lastDirection = Vector2.one.normalized;

    public Vector2 LastDirection => _lastDirection;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxisRaw(MyConst.Horizontal), Input.GetAxisRaw(MyConst.Vertical)).normalized;

        if (_direction != Vector2.zero)
        {
            _lastDirection = _direction;
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _direction * _speed;
    }
}
