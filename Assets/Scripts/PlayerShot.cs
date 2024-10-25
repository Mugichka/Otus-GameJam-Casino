using System.Collections;
using UnityEngine;

public sealed class PlayerShot : MonoBehaviour
{
    [SerializeField] private ObjectPool _chipPool;
    [SerializeField] private float _chipSpeed;
    [SerializeField] private float _shotDelay;

    private Vector3 _mousePosition;
    private Vector2 _direction;

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
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0f;
        _direction = (_mousePosition - transform.position).normalized;
        GameObject chip = _chipPool.GetObjectFromPool();
        chip.transform.position = transform.position;
        Rigidbody2D rb = chip.GetComponent<Rigidbody2D>();
        rb.velocity = _direction * _chipSpeed;
    }
}
