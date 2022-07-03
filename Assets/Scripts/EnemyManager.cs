using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> spawnPositions = new List<Vector3>();
    [SerializeField] private List<Soldier> enemiesPrefabs = new List<Soldier>();
    [SerializeField] private float spawnTime = 5;
    [SerializeField] private float spawnTimeRemaining = 0;

    private void Start()
    {
        spawnTimeRemaining = spawnTime;
    }

    private void Update()
    {
        Spawning();
    }

    private void Spawning()
    {
        if (spawnTimeRemaining > 0)
        {
            spawnTimeRemaining -= Time.deltaTime;
        }
        else
        {
            SpawnEnemy(0);
            spawnTimeRemaining = spawnTime;
        }
    }

    private void SpawnEnemy(int prefabIndex)
    {
        int randomSpawnIndex = Random.Range(0, spawnPositions.Count);

        Soldier enemyPrefab = enemiesPrefabs[prefabIndex]; 

        Vector3 newPos = spawnPositions[randomSpawnIndex];

        Instantiate(enemyPrefab, newPos, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
    }
}
