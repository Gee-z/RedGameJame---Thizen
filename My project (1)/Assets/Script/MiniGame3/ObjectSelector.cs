using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public void SelectObject(Vector3 _pointer)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(_pointer, Vector2.zero, 0f, layerMask);
        Debug.Log("Hit: " + hit1.collider?.name);
        if (hit1.collider != null)
        {
            DragAndDrop.instance.isTarget = hit1.collider.transform.parent;
        }
    }
}
