using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI pressToPlayText;
    private float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        Color originalColor = pressToPlayText.color;

        while (true)
        {
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
                pressToPlayText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            t = 0f;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
                pressToPlayText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }
        }
    }
}
