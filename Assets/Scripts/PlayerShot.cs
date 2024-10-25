using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public sealed class PlayerShot : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _shotDelay;

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
        Chip chip = _objectPoolManager.ChipPool.GetObjectFromPool();
        StartCoroutine(chip.Run(transform));
    }
}
