using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public int number;
    public Vector2Int gridPosition;
    private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        if(spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
