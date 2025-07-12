using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotChecker : MonoBehaviour
{
    public UnityEvent UnoccupiedAllSlot;
    public UnityEvent SetOccupation;
    public static SlotChecker instance;
    void Awake()
    {
        instance = this;
    }
    public void updateSlot()
    {
        UnoccupiedAllSlot.Invoke();
        SetOccupation.Invoke();
    }
}
