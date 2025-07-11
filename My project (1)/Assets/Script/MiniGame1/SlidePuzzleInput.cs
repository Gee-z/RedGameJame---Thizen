using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlidePuzzleInput : MonoBehaviour
{
    [SerializeField] private Camera isCamera;
    [SerializeField] private PuzzleManager manager;
    [SerializeField] private Vector2 frameMin = new Vector2(-1.663f, -1.52f);
    [SerializeField] private Vector2 frameMax = new Vector2(1.6848f, 1.693f);
    private Vector2 pointerPos;
    private PuzzleTile selectedTile;
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;

    public void OnPointer(InputAction.CallbackContext context)
    {
        Debug.Log("Here");
        pointerPos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log($"OnClick phase: {context.phase}");

        if (manager.puzzleIsSolved) return;

        if (context.performed)
        {
            swipeStartPos = pointerPos;

            Vector3 screenPoint = new Vector3(pointerPos.x, pointerPos.y,isCamera.nearClipPlane);
            Vector3 worldPoint = isCamera.ScreenToWorldPoint(screenPoint);

            Collider2D hit = Physics2D.OverlapPoint(worldPoint);
            if (hit != null)
            {
                selectedTile = hit.GetComponent<PuzzleTile>();
            }
            else
            {
                selectedTile = null;
            }
        }
        else if (context.canceled)
        {
            if (selectedTile != null)
            {
                swipeEndPos = pointerPos;
                Vector2 swipeDelta = swipeEndPos - swipeStartPos;

                Debug.Log($"Swipe delta: {swipeDelta}");

                if (swipeDelta.magnitude > 50f)
                {
                    Vector2 swipeDir = swipeDelta.normalized;

                    Debug.Log($"SwipeDir: {swipeDir}");

                    if (Mathf.Abs(swipeDir.x) > Mathf.Abs(swipeDir.y))
                    {
                        if (swipeDir.x > 0)
                            manager.TryMoveTileInDirection(selectedTile, Vector2Int.right);
                        else
                            manager.TryMoveTileInDirection(selectedTile, Vector2Int.left);
                    }
                    else
                    {
                        if (swipeDir.y > 0)
                            manager.TryMoveTileInDirection(selectedTile, Vector2Int.down);
                        else
                            manager.TryMoveTileInDirection(selectedTile, Vector2Int.up);
                    }
                }


                selectedTile = null;
            }
        }
    }
}
