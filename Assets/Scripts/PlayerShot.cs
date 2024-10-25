using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerShot : MonoBehaviour
{
    [SerializeField] private ObjectPool _chipPool;
    [SerializeField] private float _shotDelay;

    private Rigidbody2D _rigidBody;
    private Vector3 _mousePosition;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(_shotDelay);
            Shot();
        }
    }

    private void Shot()
    {
        GameObject chipObj = _chipPool.GetObjectFromPool();
        Chip chip = chipObj.GetComponent<Chip>();
        StartCoroutine(chip.Run(transform));
    }
}
