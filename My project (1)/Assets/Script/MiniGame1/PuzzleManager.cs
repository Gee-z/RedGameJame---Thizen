using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField] private PuzzleTile tilePrefab;
    [SerializeField] private Sprite[] tileSprites;
    [SerializeField] private Transform[] slotPositions;

    private PuzzleTile[,] tiles;
    private Vector2Int emptyGridPosition;
    public Vector2Int emptyGrid => emptyGridPosition;

    private bool puzzleSolved = false;
    public bool puzzleIsSolved => puzzleSolved;
    [SerializeField] private Vector3 emptyFillScale = Vector3.one;

    private int gridWidth;
    private int gridHeight;

    private int[,] startArrangement = new int[3, 3]
    {
        {1,2,0},
        {4,8,3},
        {7,6,5}
    };

    private int[,] finishArrangement = new int[3, 3]
    {
        {1,2,3},
        {4,5,6},
        {7,8,0}
    };

    public void Start()
    {
        puzzleSolved = false;
        BuildPuzzle();
    }

    private void BuildPuzzle()
    {
        gridHeight = startArrangement.GetLength(0);
        gridWidth = startArrangement.GetLength(1);

        tiles = new PuzzleTile[gridWidth, gridHeight];

        for(int i = 0;i< gridHeight; i++) 
        {
            for(int j = 0;j< gridWidth; j++)
            {
                int num = startArrangement[i,j];

                int slotIndex = i * gridWidth + j;
                Transform slotTransform = slotPositions[slotIndex];

                if(num == 0)
                {
                    emptyGridPosition = new Vector2Int(j,i);
                    tiles[j, i] = null;
                    continue;
                }

                PuzzleTile newTile = Instantiate(tilePrefab, slotTransform.position, Quaternion.identity);
                newTile.number = num;
                newTile.gridPosition = new Vector2Int(j, i);
                newTile.SetSprite(tileSprites[num - 1]);
                tiles[j, i] = newTile;
            }
        }
    }

    public bool IsAdjacent(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) == 1; 
    }

    public bool IsTileAdjacentToEmpty(PuzzleTile tile)
    {
        return Mathf.Abs(tile.gridPosition.x - emptyGridPosition.x) + Mathf.Abs(tile.gridPosition.y - emptyGridPosition.y) == 1;
    }

    public Vector2Int GetEmptyGridPosition()
    {
        return emptyGridPosition;
    }

    public Vector3 GetEmptySlotWorldPosition()
    {
        int slotIndex = emptyGridPosition.y * gridWidth + emptyGridPosition.x;
        return slotPositions[slotIndex].position;
    }

    public void TryMoveTileInDirection(PuzzleTile tile, Vector2Int direction)
    {
        if (puzzleSolved || tile == null) return;

        Vector2Int targetPos = tile.gridPosition + direction;

        if (targetPos.x < 0 || targetPos.x >= gridWidth ||
            targetPos.y < 0 || targetPos.y >= gridHeight)
            return;

        if (targetPos == emptyGridPosition)
        {
            tiles[emptyGridPosition.x, emptyGridPosition.y] = tile;
            tiles[tile.gridPosition.x, tile.gridPosition.y] = null;

            Vector2Int previousEmpty = emptyGridPosition;
            emptyGridPosition = tile.gridPosition;

            int emptySlotIndex = previousEmpty.y * gridWidth + previousEmpty.x;
            Vector3 targetWorldPos = slotPositions[emptySlotIndex].position;

            StartCoroutine(SlideTile(tile, targetWorldPos, 0.1f));  
            tile.gridPosition = previousEmpty;


        }
    }

    private IEnumerator SlideTile(PuzzleTile tile, Vector3 targetPos, float duration)
    {
        Vector3 startPos = tile.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            tile.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        tile.transform.position = targetPos;
        CheckWin();
    }
    private void CheckWin()
    {
        bool win = true;

        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                int expectedNum = finishArrangement[i, j];

                if (expectedNum == 0)
                {
                    if (emptyGridPosition != new Vector2Int(j, i))
                        win = false;
                }
                else
                {
                    var tile = tiles[j, i];
                    if (tile == null || tile.number != expectedNum)
                    {
                        win = false;
                    }
                }
            }
        }

        if (win)
        {
            Debug.Log("You win!");
            Win();
        }
    }

    private void Win()
    {
        puzzleSolved = true;
        Debug.Log("Puzzle solved!");

        int emptySlotIndex = emptyGridPosition.y * gridWidth + emptyGridPosition.x;
        var fillTile = new GameObject("EmptyFill");
        fillTile.transform.position = slotPositions[emptySlotIndex].position;
        var sr = fillTile.AddComponent<SpriteRenderer>();
        sr.sprite = tileSprites[8];
        sr.sortingOrder = 1;

        fillTile.transform.localScale = emptyFillScale;
    }
}
