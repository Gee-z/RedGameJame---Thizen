using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedGameData : MonoBehaviour
{
    public static SavedGameData instance;
    public int playerScore;
    public int playerCoin;
    public float playerSpeed;
    public float currentObstacleSpeed;
    public float currentObstacleSpawnInterval;
    public float currentTime;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
