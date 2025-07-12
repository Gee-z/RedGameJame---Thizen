using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecurityClickHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private SecurityTimeManager gameManager;

    private Vector2 pointerPos;

    public void OnPointer(InputAction.CallbackContext context)
    {
        pointerPos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Vector3 screenPoint = new Vector3(pointerPos.x, pointerPos.y, mainCamera.transform.position.z * -1);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);
        worldPoint.z = 0f;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit != null)
        {
            if (hit.CompareTag("Approve"))
            {
                gameManager.OnApprove();
            }
            else if (hit.CompareTag("Reject"))
            {
                gameManager.OnReject();
            }
        }
    }
}
