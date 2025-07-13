using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SavedGameData : MonoBehaviour
{
    public static SavedGameData instance;
    public UnityEvent onHpReduced;
    public int playerCoin;
    public float playerSpeed;
    public float defaultObstacleSpeed = 5f;
    public float defaultObstacleSpawnInterval = 5f;
    public float defaultTime = 60f;
    public float defaultMult = 1f;
    public int playerScore;
    public int playerHp;
    public int defaultHp = 5;
    public float currentObstacleSpeed;
    public float currentObstacleSpawnInterval;
    public float currentTime;
    public GameObject Shield;
    public bool haveShield = false;
    public bool isBig = false;

    public float currentMult = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddCoin(int amount = 1)
    {
        CoinUIManager.instance?.PlayCollectAnimation();
        playerCoin += (int)(amount * currentMult);
    }
    public void AddScore(int amount = 1)
    {
        playerScore += (int)(amount * currentMult);
    }
    public void ReduceHP(int amount = 1)
    {
        if (haveShield)
        {
            haveShield = false;
            return;
        }
        if (isBig)
        {
            AddCoin(10);
            AddScore(50);
            return;
        }
        onHpReduced.Invoke();
        SlideSound.instance.PlayHurt();
        playerHp -= amount;
    }
    public void Update()
    {
        playerHp = Mathf.Clamp(playerHp, 0, 5);
        if (playerHp == 0)
        {
            WaterParkGameOver.instance.ShowGameOver();
        }
    }
    public void ResetGameData()
    {
        currentObstacleSpeed = defaultObstacleSpeed;
        currentObstacleSpawnInterval = defaultObstacleSpawnInterval;
        currentTime = defaultTime;
        currentMult = defaultMult;
        playerScore = 0;
        playerHp = defaultHp;
        haveShield = false;
        isBig = false;
    }

}
