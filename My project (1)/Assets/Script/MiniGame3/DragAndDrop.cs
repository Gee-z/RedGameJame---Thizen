using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    public static DragAndDrop instance;
    [HideInInspector]
    public Transform isTarget;
    [SerializeField] private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragOffset;
    [HideInInspector]
    public Vector2 pointerPos;
    private Vector2 savedPosition;
    public ObjectSelector objectSelector;
    private void Awake()
    {
        instance = this;
    }
    public void OnClick(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            if (isTarget != null)
            {
                return;
            }
            objectSelector.SelectObject(mainCamera.ScreenToWorldPoint(pointerPos));
            if (isTarget == null)
            {
                return;
            }
            if (!isDragging)
            {
                SlotChecker.instance.updateSlot();
                isDragging = true;
                Vector3 worldPos = mainCamera.ScreenToWorldPoint(pointerPos);
                dragOffset = worldPos - isTarget.position;
                savedPosition = isTarget.position;
            }
        }
        else if (context.canceled)
        {
            if (isTarget == null)
            {
                return;
            }
            isDragging = false;
            if (isTarget.GetComponent<Object>().checkOccupation(savedPosition))
            {
                Debug.Log("ItWok");
                isTarget.GetComponent<Object>().SetOccupation(true);
                isTarget.GetComponent<Object>().moveToSlot();
            }
            else
            {
                isTarget.GetComponent<Object>().SetOccupation(true);
            }

            isTarget = null;
        }
    }

    public void OnPointer(InputAction.CallbackContext context)
    {
        pointerPos = context.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(pointerPos);
            isTarget.position = worldPos - dragOffset;
        }
    }
}
