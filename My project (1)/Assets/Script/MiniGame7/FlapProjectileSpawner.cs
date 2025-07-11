using System.Collections;
using System.Collections.Generic;
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

    [Header("Timing")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float warningDuration = 1f;
    [SerializeField] private float warningBlinkRate = 0.2f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            float y = Random.Range(bottomYTransform.position.y, topYTransform.position.y);
            float x = xTransform.position.x;

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

            Vector3 spawnPos = new Vector3(x, y, 0f);
            Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        }
    }
}

