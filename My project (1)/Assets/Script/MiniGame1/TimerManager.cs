using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float timeLimit = 15f;
    private float timer;

    public TMP_Text timerText;
    private bool isRunning = true;
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (!isRunning) return;

        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        
    }

    public void StopTimer()
    {
        FindObjectOfType<PuzzleManager>()?.Lose();
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = timeLimit;
        isRunning = true;
        Time.timeScale = 1f;
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }
    }
}
