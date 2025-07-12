using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaterTimer : MonoBehaviour
{
    public float timeLimit = 30f;
    private float timer;

    public TMP_Text timerText;
    private bool gameEnded = false;

    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (gameEnded) return;
        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0)
        {
            gameEnded = true;
            timerText.text = "You Win!";
            Time.timeScale = 0f;
        }
    }

    public void StopTimer()
    {
        gameEnded = true;
        timerText.gameObject.SetActive(false);
    }
}
