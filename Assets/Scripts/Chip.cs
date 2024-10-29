using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Chip : MonoBehaviour, IDamageBuffable, ISpeedBuffable
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _chipSpeed;
    [SerializeField] private int _damage;

    private ObjectPoolManager _objectPoolManager;
    private ChipTrigger _chipTrigger;
    private Rigidbody2D _rigidBody;
    public bool buffApplied = false;

    public int Damage => _damage;

    public void ApplyDamageBuff(float buffAmount)
    {
        _damage += (int)buffAmount;
        Debug.Log("Chip damage increased to: " + _damage);
    }

    public void ApplySpeedBuff(float buffAmount)
    {
        _chipSpeed += buffAmount;
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _chipTrigger = GetComponent<ChipTrigger>();
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
    }

    private void OnEnable()
    {
        _chipTrigger.ChipTriggered += Die;
    }

    private void OnDisable()
    {
        _chipTrigger.ChipTriggered -= Die;
    }

    private void Die()
    {
        _objectPoolManager.ChipPool.ReturnObjectToPool(this);
    }

    public void Run(Transform playerTransform, float angleOffset)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        Vector3 rotatedDirection = Quaternion.Euler(0, 0, angleOffset) * direction;
        transform.position = playerTransform.position;
        _rigidBody.velocity = rotatedDirection * _chipSpeed;

        StartCoroutine(RunLifetime());
    }

    private IEnumerator RunLifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Die();
    }
}
