using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class WaterParkGameOver : MonoBehaviour
{
    public GameObject blackScreen;
    public TextMeshProUGUI failText1;           
    public TextMeshProUGUI failText2;
    public GameObject upgradeObject;
    public GameObject retryObject;
    public float fadeDuration = 1.5f;
    private SpriteRenderer blackScreenRenderer;
    void Start()
    {
        blackScreenRenderer = blackScreen.GetComponent<SpriteRenderer>();

        failText1.gameObject.SetActive(false);
        failText2.gameObject.SetActive(false);
        retryObject.SetActive(false); 
        upgradeObject.SetActive(false); 
    }

    public void ShowGameOver()
    {
        StartCoroutine(FadeAndShowText());
    }

    IEnumerator FadeAndShowText()
    {

        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            SetAlpha(blackScreenRenderer, alpha);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        SetAlpha(blackScreenRenderer, 1f);

        retryObject.SetActive(true);
        upgradeObject.SetActive(true);
        failText1.gameObject.SetActive(true);
        failText2.gameObject.SetActive(true);
    }

    void SetAlpha(SpriteRenderer sr, float alpha)
    {
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Selection");
    }
}
