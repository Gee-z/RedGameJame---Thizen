using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputFlap : MonoBehaviour
{
    [SerializeField] private BirdGameManager gameManager;

    private bool isHolding;

    public void OnFloat(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            isHolding = false;
        }

        gameManager.HandleFloatInput(isHolding);
    }
}
