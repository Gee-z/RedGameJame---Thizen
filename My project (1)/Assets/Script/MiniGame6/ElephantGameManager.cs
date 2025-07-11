using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantGameManager : MonoBehaviour
{
    [Header("Food Table Items")]
    [SerializeField] private List<FoodItem> foodItems;

    [Header("Elephant Thought Bubble")]
    [SerializeField] private SpriteRenderer thinkingSprite;

    private string currentTargetID;
    private void Start()
    {
        PickNextTarget();
    }

    public void ClickedFood(Vector2 worldPoint)
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit != null)
        {
            FoodItem food = hit.GetComponent<FoodItem>();
            if (food != null && food.itemID == currentTargetID)
            {
                foodItems.Remove(food);
                Destroy(food.gameObject);

                if (foodItems.Count > 0)
                    PickNextTarget();
                else
                    Debug.Log("All food eaten!");
            }
        }
    }

    private void PickNextTarget()
    {
        if (foodItems.Count == 0) return;
        int index = Random.Range(0, foodItems.Count);
        FoodItem targetFood = foodItems[index];
        currentTargetID = targetFood.itemID;
        thinkingSprite.sprite = targetFood.GetComponent<SpriteRenderer>().sprite;
    }
}
