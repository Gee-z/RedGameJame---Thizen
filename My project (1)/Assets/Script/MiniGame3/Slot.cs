using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied = false;
    public bool PermanentOccupied = false;
    void Start()
    {
        isOccupied = PermanentOccupied;
        SlotChecker.instance.UnoccupiedAllSlot.AddListener(setOccupationToFalse);
    }
    void OnDisable()
    {
        SlotChecker.instance.UnoccupiedAllSlot.RemoveListener(setOccupationToFalse);
    }
    public void setOccupationToFalse()
    {
        if (!PermanentOccupied)
        {
            isOccupied = false;
        }
    }
}
