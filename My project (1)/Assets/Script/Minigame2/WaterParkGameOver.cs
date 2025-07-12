using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterParkGameOver : MonoBehaviour
{
    public GameObject blackScreen;
    public GameObject failText;         
    public GameObject backToMenuObject;
    public float fadeDuration = 1.5f;
    public float textDropSpeed = 200f;
    private Vector3 textStartPos;
    private Vector3 textTargetPos;
    void Start()
    {
        blackScreen.SetActive(true);
        SetAlpha(blackScreen.GetComponent<SpriteRenderer>(), 0f);
        failText.SetActive(false);
        backToMenuObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeAndShowText());
    }

    IEnumerator FadeAndShowText()
    {

        SpriteRenderer sr = blackScreen.GetComponent<SpriteRenderer>();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            SetAlpha(sr, alpha);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        SetAlpha(sr, 1f);

        failText.SetActive(true);
        Transform textTf = failText.transform;
        textStartPos = textTf.position + new Vector3(0, 5, 0);
        textTargetPos = textTf.position;
        textTf.position = textStartPos;

        timer = 0f;
        while (timer < 1f)
        {
            textTf.position = Vector3.Lerp(textStartPos, textTargetPos, timer);
            timer += Time.unscaledDeltaTime * (textDropSpeed / 100f);
            yield return null;
        }

        textTf.position = textTargetPos;
        backToMenuObject.SetActive(true);
    }

    private void SetAlpha(SpriteRenderer sr, float alpha)
    {
        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Selection");
    }
}
