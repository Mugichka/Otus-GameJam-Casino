using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed; 
 
    private Rigidbody2D _rigidBody;
    private Vector2 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = new Vector2(Input.GetAxisRaw(MyConst.Horizontal), Input.GetAxisRaw(MyConst.Vertical)).normalized;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _direction * _speed;
    }
}
