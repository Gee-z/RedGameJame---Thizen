using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    private string[] miniGames = new string[]
    {
        "MiniGame1",
        "MiniGame3",
        "MiniGame4",
        "MiniGame6",
        "MiniGame7"
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            int randomIndex = Random.Range(0, miniGames.Length);
            string chosenMiniGame = miniGames[randomIndex];
            SceneManager.LoadScene(chosenMiniGame);
        }
    }
}
