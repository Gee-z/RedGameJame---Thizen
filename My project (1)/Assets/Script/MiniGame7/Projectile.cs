using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -30f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hit");
            StartCoroutine(LoseGame());
        }
    }
    private IEnumerator LoseGame()
    {
        yield return new WaitForSeconds(0.8f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MiniGame2");
    }
}
