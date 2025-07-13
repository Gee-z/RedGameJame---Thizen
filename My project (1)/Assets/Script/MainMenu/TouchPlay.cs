using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TouchPlay : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public GameObject pressToPlayText;
    private Vector2 pointerPos;
    private bool hasClicked;
    void Start()
    {
        hasClicked = false;
        pressToPlayText.SetActive(true);
    }
    public void OnPointer(InputAction.CallbackContext context)
    {
        pointerPos = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (hasClicked || !context.performed) return;
        Vector3 screenPoint = new Vector3(pointerPos.x, pointerPos.y, mainCamera.nearClipPlane);
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(screenPoint);

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit != null)
        {
            hasClicked = true;

            if (pressToPlayText != null)
                pressToPlayText.SetActive(false);

            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
