using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacleMove : MonoBehaviour
{
    public WaterParkGameOver gameOverHandler;
    void Update()
    {


    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            Time.timeScale = 0f;
            if (gameOverHandler != null)
            {
                gameOverHandler.ShowGameOver(); 
            }

        }
    }
}
