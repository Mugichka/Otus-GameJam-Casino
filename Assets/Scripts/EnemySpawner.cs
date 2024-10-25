using UnityEngine;
using System.Collections;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _spawnDelay;

    private Vector3 _screenBottomLeft;
    private Vector3 _screenTopRight;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            SpawnEnemy();
        }
    }

    private void UpdateScreenBounds()
    {
        _screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private Vector3 GetRandomSpawnPosition()
    {
        UpdateScreenBounds();

        float xPos = 0f;
        float yPos = 0f;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0:
                xPos = _screenBottomLeft.x;
                yPos = Random.Range(_screenBottomLeft.y, _screenTopRight.y);
                break;
            case 1:
                xPos = _screenTopRight.x;
                yPos = Random.Range(_screenBottomLeft.y, _screenTopRight.y);
                break;
            case 2:
                xPos = Random.Range(_screenBottomLeft.x, _screenTopRight.x);
                yPos = _screenTopRight.y;
                break;
            case 3:
                xPos = Random.Range(_screenBottomLeft.x, _screenTopRight.x);
                yPos = _screenBottomLeft.y;
                break;
        }

        return new Vector3(xPos, yPos, 0);
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemy = _objectPoolManager.EnemyPool.GetObjectFromPool();
        enemy.transform.position = spawnPosition;
    }
}
