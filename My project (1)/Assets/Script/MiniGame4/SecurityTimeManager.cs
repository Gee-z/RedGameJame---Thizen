using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SecurityTimeManager : MonoBehaviour
{
    public float timeLimit = 30f;
    private float timer;

    public int score = 0;
    public TMP_Text timerText, scoreText;
    public SecurityItemSpawner itemSpawner;

    private ItemGroup currentGroup;
    private bool inputEnabled = false;

    void Start()
    {
        timer = timeLimit;
        Time.timeScale = 1f;
        NextGroup();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timer).ToString();

        if (timer <= 0)
            EndGame(false);
    }

    public void OnApprove() => Evaluate(true);
    public void OnReject() => Evaluate(false);

    void Evaluate(bool isApproval)
    {
        if (!inputEnabled) return;

        inputEnabled = false;
        bool isIllegal = currentGroup.HasIllegalItem();
        bool isCorrect = (isApproval && !isIllegal) || (!isApproval && isIllegal);
        bool slideRight = !isApproval;

        if (isCorrect)
        {
            score++;
            scoreText.text = $"{score}/10";
        }

        currentGroup.SlideOff(isCorrect ? !slideRight : slideRight);

        if (score >= 10)
            EndGame(true);
        else
            Invoke(nameof(NextGroup), 0.6f);
    }

    void NextGroup()
    {
        currentGroup = itemSpawner.SpawnGroup();
        currentGroup.SlideIn(itemSpawner.centerPoint.position, () =>
        {
            inputEnabled = true; 
        });
    }

    void EndGame(bool win)
    {
        timerText.text = win ? "You Win!" : "Time's Up!";
        Time.timeScale = 0f;
    }
}
