using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCoin : MonoBehaviour
{

    public int coinAmount = 1;
    private bool isCollected = false;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isCollected || !other.CompareTag("Player")) return;

        isCollected = true;
        SavedGameData.instance.AddCoin(coinAmount);
        Destroy(gameObject);
    }
}
