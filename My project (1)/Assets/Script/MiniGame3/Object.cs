using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    void Start()
    {
        SlotChecker.instance.SetOccupation.AddListener(SetToOccupied);
        ObjectSpawnLoc = transform.position;
    }
    void OnDisable()
    {
        SlotChecker.instance.SetOccupation.RemoveListener(SetToOccupied);
    }
    public Vector2 ObjectSpawnLoc;
    public List<Transform> otherSlotHitbox = new List<Transform>();
    [SerializeField] private LayerMask layerMask;
    public void SetToOccupied()
    {
        if (DragAndDrop.instance.isTarget != transform)
        {
            SetOccupation(true);
        }
    }
    public bool checkOccupation(Vector2 PreLoc)
    {
        int occupiedCount = 0;
        foreach (Transform slot in otherSlotHitbox)
        {
            Vector2 point = slot.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f, layerMask);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.GetComponent<Slot>().isOccupied)
                {
                    occupiedCount++;
                }
            }
            else
            {
                transform.position = ObjectSpawnLoc;
                return false;
            }
        }
        Vector2 point1 = transform.position;
        RaycastHit2D hit1 = Physics2D.Raycast(point1, Vector2.zero, 0f, layerMask);
        if (hit1.collider != null)
        {
            if (hit1.collider.GetComponent<Slot>().isOccupied)
            {
                occupiedCount++;
            }
        }
        else
        {
            transform.position = ObjectSpawnLoc;
            return false;
        }
        if (occupiedCount == 0)
        {
            return true;
        }
        transform.position = PreLoc;
        return false;
    }
    public void SetOccupation(bool isOccupied)
    {
        Vector2 point1 = transform.position;
        RaycastHit2D hit1 = Physics2D.Raycast(point1, Vector2.zero, 0f, layerMask);
        if (hit1.collider != null)
        {
            hit1.collider.GetComponent<Slot>().isOccupied = isOccupied;
        }
        foreach (Transform slot in otherSlotHitbox)
        {
            Vector2 point = slot.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f, layerMask);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Slot>().isOccupied = isOccupied;
            }
        }
    }
    public void moveToSlot()
    {
        Vector2 point = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.zero, 0f, layerMask);
        if (hit.collider != null)
        {
            transform.position = hit.transform.position;
        }
    }
}
