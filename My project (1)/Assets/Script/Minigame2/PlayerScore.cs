using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Clamp(0.8f - SavedGameData.instance.currentObstacleSpeed,0.3f,3f));
            score++;
            SavedGameData.instance.playerScore = score;
        }
    }
}
