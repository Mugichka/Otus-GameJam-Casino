using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class Chip : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    private GameObject _chipPool;
    private ChipTrigger _chipTrigger;

    public int Damage => _damage;

    private void Awake()
    {
        _chipTrigger = GetComponent<ChipTrigger>();
        _chipPool = GameObject.FindWithTag(MyConst.ChipPool);
    }

    private void OnEnable()
    {
        _chipTrigger.ChipTriggered += Die;
        StartCoroutine(Run());
    }

    private void OnDisable()
    {
        _chipTrigger.ChipTriggered -= Die;
    }

    private void Die()
    {
        _chipPool.GetComponent<ObjectPool>().ReturnObjectToPool(gameObject);
    }

    public IEnumerator Run()
    {
        yield return new WaitForSeconds(_lifeTime);
        Die();
    }
}
