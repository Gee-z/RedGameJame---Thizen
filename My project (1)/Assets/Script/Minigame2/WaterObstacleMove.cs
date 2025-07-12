using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacleMove : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }

    }

    public void DestroyObs()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
