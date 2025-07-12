using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualAnimation : MonoBehaviour
{
    public List<Sprite> sprites;
    public float frameRate = 0.1f;
    IEnumerator Start()
    {
        while (true)
        {
            foreach (Sprite sprite in sprites)
            {
                GetComponent<SpriteRenderer>().sprite = sprite;
                yield return new WaitForSeconds(Random.Range(frameRate, frameRate + 0.05f));
            }
            yield return new WaitForSeconds(Random.Range(0.02f, 0.05f)); // Random delay between loops
        }
    }

}
