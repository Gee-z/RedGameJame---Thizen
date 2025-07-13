using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotWinChecker : MonoBehaviour
{
    public float countdownTime = 20f;
    private float timer;
    private bool finish = false;
    private Slot[] allSlots;

    public TextMeshProUGUI timerText; 

    void Start()
    {
        timer = countdownTime;
        allSlots = FindObjectsOfType<Slot>();
    }

    void Update()
    {
        if (finish) return;

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            timer = 0f;
        }

        UpdateTimerText();

        if (CheckAllSlotsOccupied())
        {
            finish = true;
            Win();
        }
        else if (timer <= 0f)
        {
            finish = true;
            Lose();
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timer).ToString();
        }
    }

    bool CheckAllSlotsOccupied()
    {
        foreach (Slot slot in allSlots)
        {
            if (!slot.isOccupied && !slot.PermanentOccupied)
                return false;
        }
        return true;
    }

    void Win()
    {
        Debug.Log(":D");
        timerText.text = "WIN!";
    }

    void Lose()
    {
        Debug.Log(":(");
        timerText.text = "TIME'S UP!";
    }
}
