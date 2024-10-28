using UnityEngine;

public sealed class BawlingBall : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    //[SerializeField] private ObjectPoolManager _objectPoolManager;

    //private void Update()
    //{
    //    transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    //}

    public void Run(Transform playerTransform, Vector2 playerDirection)
    {
        transform.position = playerTransform.position;
        _rigidbody.velocity = playerDirection * _ballSpeed;
    }
}
