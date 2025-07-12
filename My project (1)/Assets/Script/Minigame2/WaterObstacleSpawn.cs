using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float spawnInterval = 3f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        int randomLane = Random.Range(0, spawnPositions.Length);
        GameObject obstacleToSpawn = obstaclePrefabs[randomLane];
        Transform spawnPoint = spawnPositions[randomLane];

        Instantiate(obstacleToSpawn, spawnPoint.position, obstacleToSpawn.transform.rotation);
    }
}
