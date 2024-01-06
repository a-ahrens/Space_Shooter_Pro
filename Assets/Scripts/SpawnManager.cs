using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField] 
    private GameObject _enemyContainer;

    [SerializeField]
    private float _enemySpawnRate = 5.0f;
    private bool _stopSpawning = false;

    [SerializeField] private GameObject [] _powerups;

    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while ( _stopSpawning == false )
        {
            Vector3 spawnPosition = CalcSpawnPosition();
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(7.0f);
            int powerupOption = Random.Range(0, _powerups.Length);
            Vector3 spawnPosition = CalcSpawnPosition();
            Instantiate(_powerups[powerupOption], spawnPosition, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    private Vector3 CalcSpawnPosition()
    {
        float randomX = Random.Range(-10.3f, 10.3f);
        return new Vector3(randomX, 7.36f, 0);
    }
}
