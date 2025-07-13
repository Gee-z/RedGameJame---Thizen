using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        string previousScene = PlayerPrefs.GetString("LastScene", "MiniGame2");
        SceneManager.LoadScene(previousScene);
    }
}
