using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputWaterPark : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerMove playerMover;

    private Vector2 pointerPos;
    public void OnPointer(InputAction.CallbackContext context)
    {
        pointerPos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector3 screenPoint = new Vector3(pointerPos.x, pointerPos.y, -mainCamera.transform.position.z);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        worldPoint.z = 0f;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit != null)
        {
            if (hit.CompareTag("Left"))
            {
                playerMover.MoveLeft();
            }
            else if (hit.CompareTag("Right"))
            {
                playerMover.MoveRight();
            }
        }
    }
}
