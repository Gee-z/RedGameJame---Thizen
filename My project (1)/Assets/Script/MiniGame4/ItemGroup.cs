using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGroup : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private bool slidingOff = false;
    private bool slidingIn = false;

    private Vector3 targetPosition;
    private System.Action onSlideInComplete;
    private float slideSpeed = 8f;

    void Update()
    {
        if (slidingIn)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                slidingIn = false;
                onSlideInComplete?.Invoke();
            }
        }

        if (slidingOff)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SlideOff(bool toRight)
    {
        float direction = toRight ? 1f : -1f;
        targetPosition = transform.position + Vector3.left * 12f * direction; 
        slidingOff = true;
    }

    public void SlideIn(Vector3 destination, System.Action onComplete = null)
    {
        transform.position = destination + Vector3.right * 12f; 
        targetPosition = destination;
        slidingIn = true;
        onSlideInComplete = onComplete;
    }

    public bool HasIllegalItem()
    {
        foreach (var item in items)
        {
            if (item == null) continue; 
            if (item.isIllegal) return true;
        }
        return false;
    }
}
