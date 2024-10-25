using UnityEngine;
using System.Collections.Generic;

public sealed class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize = 10;

    private Queue<GameObject> objectPool;
    private GameObject _root;

    void Start()
    {
        _root = new GameObject($"{_prefab.name}PoolRoot");
        objectPool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab, _root.transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_prefab, _root.transform);
            return obj;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
