using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacleSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] coinPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float spawnInterval = 3f;
    public float obstacleSpeed = 1f;
    public float startValue = 0.1f;
    public float endValue = 3f;
    public float startValueInterval = 0.1f;
    public float endValueInterval = 3f;
    public float duration = 60f;

    private float _elapsedTime;


    void Update()
    {
        if (_elapsedTime < duration)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / duration;
            obstacleSpeed = Mathf.Lerp(startValue, endValue, t);
            spawnInterval = Mathf.Lerp(startValueInterval, endValueInterval, t);
            saveData();
        }
    }
    private IEnumerator Start()
    {
        startValueInterval = SavedGameData.instance.currentObstacleSpawnInterval;
        startValue =  SavedGameData.instance.currentObstacleSpeed;
        duration = SavedGameData.instance.currentTime;
        spawnInterval = startValueInterval;
        obstacleSpeed = startValue;
        while (true)
        {
            float savedSpeed = obstacleSpeed;
            int randomCount = Random.Range(0, 3);
            List<int> lane = new List<int> { 0, 1, 2 };
            Debug.Log(randomCount);
            if (randomCount == 0)
            {
                int randomLane = Random.Range(0, lane.Count);
                SpawnObstacle(randomLane, savedSpeed);
                lane.RemoveAt(randomLane);
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    int randomLane = Random.Range(0, lane.Count);
                    SpawnObstacle(lane[randomLane], savedSpeed);
                    lane.RemoveAt(randomLane);
                }
            }
            if (lane.Count > 0)
            {
                int randomLane = Random.Range(0, lane.Count);
                Debug.Log("Spawning Coin at " + lane[randomLane]);
                SpawnCoin(lane[randomLane], savedSpeed);
                lane.RemoveAt(randomLane);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void saveData()
    {
        SavedGameData.instance.currentObstacleSpeed = obstacleSpeed;
        SavedGameData.instance.currentObstacleSpawnInterval = spawnInterval;
        SavedGameData.instance.currentTime = duration - _elapsedTime;
    }
    private void SpawnObstacle(int lane, float speed)
    {
        GameObject obstacleToSpawn = obstaclePrefabs[lane];
        Transform spawnPoint = spawnPositions[lane];
        GameObject Spawnedbject = Instantiate(obstacleToSpawn, spawnPoint.position, obstacleToSpawn.transform.rotation);
        Spawnedbject.GetComponent<Animator>().speed = speed;
    }
    private void SpawnCoin(int lane, float speed)
    {
        GameObject obstacleToSpawn = coinPrefabs[lane];
        Transform spawnPoint = spawnPositions[lane];
        GameObject Spawnedbject = Instantiate(obstacleToSpawn, spawnPoint.position, obstacleToSpawn.transform.rotation);
        Spawnedbject.GetComponent<Animator>().speed = speed;
    }
}
