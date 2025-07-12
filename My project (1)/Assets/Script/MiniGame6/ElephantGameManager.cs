using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantGameManager : MonoBehaviour
{
    [Header("Food Table Items")]
    [SerializeField] private List<GameObject> foodItems;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Elephant Thought Bubble")]
    [SerializeField] private SpriteRenderer thinkingSprite;

    private List<FoodItem> currentFoods = new List<FoodItem>();
    private Queue<string> thoughtQueue = new Queue<string>();
    private bool acceptingInput = false;

    private void Start()
    {
        SpawnPresetFoods();
        ShowNextThought(); 
    }

    private void SpawnPresetFoods()
    {
        foreach (var food in currentFoods)
        {
            if (food != null)
                Destroy(food.gameObject);
        }

        currentFoods.Clear();
        thoughtQueue.Clear();
        acceptingInput = false;


        for (int i = 0; i < spawnPoints.Length && i < foodItems.Count; i++)
        {
            GameObject newFood = Instantiate(foodItems[i], spawnPoints[i].position, Quaternion.identity);
            FoodItem foodItem = newFood.GetComponent<FoodItem>();
            currentFoods.Add(foodItem);
            thoughtQueue.Enqueue(foodItem.itemID);
        }
    }

    private void ShowNextThought()
    {
        if (thoughtQueue.Count == 0)
        {
            thinkingSprite.sprite = null;
            Debug.Log("Win");
            acceptingInput = false;
            return;
        }

        string nextID = thoughtQueue.Peek();
        FoodItem target = currentFoods.Find(f => f != null && f.itemID == nextID);

        if (target != null)
        {
            thinkingSprite.sprite = target.GetComponent<SpriteRenderer>().sprite;
            acceptingInput = true;
        }
    }

    public void ClickedFood(Vector2 worldPoint)
    {
        if (!acceptingInput || thoughtQueue.Count == 0) return;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit != null)
        {
            FoodItem food = hit.GetComponent<FoodItem>();
            if (food != null)
            {
                if (food.itemID == thoughtQueue.Peek())
                {
                    thoughtQueue.Dequeue();
                    currentFoods.Remove(food);
                    Destroy(food.gameObject);

                    acceptingInput = false;
                    Invoke(nameof(ShowNextThought), 0.5f); 
                }
                else
                {
                    acceptingInput = false;
                    Debug.Log("Lose");
                    thinkingSprite.sprite = null;
                }
            }
        }
    }
}
