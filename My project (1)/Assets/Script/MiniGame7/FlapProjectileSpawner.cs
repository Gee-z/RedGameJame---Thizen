using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlapProjectileSpawner : MonoBehaviour
{
    [Header("Spawn Position Range y and x")]
    [SerializeField] private Transform bottomYTransform;
    [SerializeField] private Transform topYTransform;
    [SerializeField] private Transform xTransform;
    [SerializeField] private Transform xWarningTransform;

    [Header("Prefabs")]
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject coinPrefab;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;

    [Header("Coin Spawn")]
    [SerializeField] private float coinSpawnInterval = 8f;
    private float nextCoinTime = 8f;

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private float warningBlinkRate = 0.2f;

    private float timer = 0f;
    private float timerShown = 20f;
    private bool coinSpawned = false;

    private void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerShown -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(timerShown).ToString();

        if (timer >= nextCoinTime)
        {
            Instantiate(coinPrefab, xTransform.position, Quaternion.identity);
            nextCoinTime += coinSpawnInterval;
        }

        if (timer > 20)
        {
            Time.timeScale = 0f;
            Debug.Log("Win");
        }
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int count = GetSpawnCountForTime(timer);
            List<float> yPositions = GetRandomYPositions(count);

            foreach (float y in yPositions)
            {
                StartCoroutine(SpawnProjectileWithWarning(y));
            }
        }
    }

    private IEnumerator SpawnProjectileWithWarning(float y)
    {
        Vector3 warningPos = new Vector3(xWarningTransform.position.x, y, 0f);
        GameObject warning = Instantiate(warningPrefab, warningPos, Quaternion.identity);
        SpriteRenderer sr = warning.GetComponent<SpriteRenderer>();

        float elapsed = 0f;
        while (elapsed < warningDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(warningBlinkRate);
            elapsed += warningBlinkRate;
        }

        Destroy(warning);

        Vector3 spawnPos = new Vector3(xTransform.position.x, y, 0f);
        Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    }

    private int GetSpawnCountForTime(float time)
    {
        if (time >= 15f) return 4;
        if (time >= 10f) return 3;
        if (time >= 5f) return 2;
        return 1;
    }

    private List<float> GetRandomYPositions(int count)
    {
        HashSet<float> positions = new HashSet<float>();
        int attempts = 0;

        while (positions.Count < count && attempts < 20)
        {
            float y = Random.Range(bottomYTransform.position.y, topYTransform.position.y);
            positions.Add(y);
            attempts++;
        }

        return new List<float>(positions);
    }
}

