using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform leftPos;
    public Transform middlePos;
    public Transform rightPos;

    private int currentLane = 1;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    void Start()
    {
        SetTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 300f * Time.deltaTime);
    }

    private void SetTargetPosition()
    {
        switch (currentLane)
        {
            case 0:
                targetPosition = leftPos.position;
                targetRotation = Quaternion.Euler(0f, 0f, -15f);
                break;
            case 1:
                targetPosition = middlePos.position;
                targetRotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 2:
                targetPosition = rightPos.position;
                targetRotation = Quaternion.Euler(0f, 0f, 15f);
                break;
        }
    }

    public void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            SetTargetPosition();
        }
    }

    public void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            SetTargetPosition();
        }
    }
}
