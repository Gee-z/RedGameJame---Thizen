using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadZone"))
        {
            Debug.Log("hit");

            StartCoroutine(LoseGame());
        }
    }
    private IEnumerator LoseGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        string previousScene = PlayerPrefs.GetString("LastScene", "MiniGame2");
        UnityEngine.SceneManagement.SceneManager.LoadScene(previousScene);
    }
}
