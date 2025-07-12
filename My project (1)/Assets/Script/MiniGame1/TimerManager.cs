using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 15f;
    private float timer;

    public TMP_Text timerText;
    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0)
            EndGame(false);
    }

    void EndGame(bool win)
    {
        timerText.text = win ? "You Win!" : "Time's Up!";
        Time.timeScale = 0f;
    }
}
