using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputElephantGame : MonoBehaviour
{
    [SerializeField] private ElephantGameManager gameManager;
    [SerializeField] private Camera mainCamera;

    private Vector2 pointerPos;

    public void OnPointer(InputAction.CallbackContext context)
    {
        Debug.Log("Here");
        pointerPos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector3 screenPoint = new Vector3(pointerPos.x, pointerPos.y, mainCamera.nearClipPlane);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        worldPoint.z = 0f;

        gameManager.ClickedFood(worldPoint);
    }
}
