using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

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
        if (CheckAllSlotsOccupied() && !finish)
        {
            finish = true;
            StartCoroutine(isWin());
        }
        else if (timer <= 0f && !finish)
        {
            finish = true;
            StartCoroutine(isLose());
        }
        UpdateTimer();
    }
    void UpdateTimer()
    {
        if (finish) return;

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            timer = 0f;
        }

        UpdateTimerText();
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

    IEnumerator isWin()
    {
        Debug.Log(":D");

        if (SavedGameData.instance != null)
        {
            SavedGameData.instance.AddCoin(50);
            StartCoroutine(SavedGameData.instance.GetComponent<PlayerPowerUp>().ChangeMult());
        }

        if (CoinUIManager.instance != null)
        {
            CoinUIManager.instance.PlayCollectAnimation();
        }
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGame2");
    }

    IEnumerator isLose()
    {
        Debug.Log(":(");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;

        string lastScene = PlayerPrefs.GetString("LastScene", "MiniGame2");
        SceneManager.LoadScene(lastScene);
    }
}
