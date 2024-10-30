using UnityEngine;
using System.Collections;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _objectPoolManager;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private FortuneWheel _fortuneWheel;

    private Vector3 _screenBottomLeft;
    private Vector3 _screenTopRight;

    private void OnEnable()
    {
        _fortuneWheel.EnemyFell += WheelSpawn;
    }

    private void OnDisable()
    {
        _fortuneWheel.EnemyFell -= WheelSpawn;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            ProgressiveSpawn();
            //SpawnNormalEnemy();
            //SpawnLightweightEnemy();
            //SpawnHeavyweightEnemy();
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

    private void SpawnNormalEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemyNormal = _objectPoolManager.EnemyNormalPool.GetObjectFromPool();
        enemyNormal.transform.position = spawnPosition;
    }

    private void SpawnLightweightEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemyLightweight = _objectPoolManager.EnemyLightweightPool.GetObjectFromPool();
        enemyLightweight.transform.position = spawnPosition;
    }

    private void SpawnHeavyweightEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Enemy enemyHeavyweight = _objectPoolManager.EnemyHeavyweightPool.GetObjectFromPool();
        enemyHeavyweight.transform.position = spawnPosition;
    }

    public void WheelSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        SpawnNormalEnemy();
        SpawnLightweightEnemy();
        SpawnHeavyweightEnemy();
    }

    private void ProgressiveSpawn()
    {
        var f=Time.timeSinceLevelLoad;
        //Debug.Log(f/60);
        for (int i = 0; i < f/60; i++)
        {
            SpawnNormalEnemy();
            SpawnLightweightEnemy();
            SpawnHeavyweightEnemy();
            
        }
        
    }
}
