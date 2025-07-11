using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGameManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D birdRb;
    [SerializeField] private float ascendSpeed = 5f;
    [SerializeField] private float descendSpeed = 5f;

    private bool isHolding;

    public void HandleFloatInput(bool holding)
    {
        isHolding = holding;
    }

    private void Update()
    {
        float verticalVelocity = isHolding ? ascendSpeed : -descendSpeed;
        birdRb.velocity = new Vector2(0f, verticalVelocity);
    }
}
