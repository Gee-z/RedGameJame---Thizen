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
    private float thoughtDelay = 1.5f;
    private bool acceptingInput = false;

    private void Start()
    {
        StartNewRound();
    }

    private void StartNewRound()
    {
        foreach (var food in currentFoods)
        {
            if (food != null)
                Destroy(food.gameObject);
        }
        currentFoods.Clear();
        thoughtQueue.Clear();
        acceptingInput = false;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rand = Random.Range(0, foodItems.Count);
            GameObject newFood = Instantiate(foodItems[rand], spawnPoints[i].position, Quaternion.identity);
            FoodItem foodItem = newFood.GetComponent<FoodItem>();
            currentFoods.Add(foodItem);
            thoughtQueue.Enqueue(foodItem.itemID);
        }

        StartCoroutine(ShowThoughts());
    }

    private IEnumerator ShowThoughts()
    {
        foreach (var foodID in thoughtQueue)
        {
            FoodItem target = currentFoods.Find(f => f.itemID == foodID);
            if (target != null)
            {
                thinkingSprite.sprite = target.GetComponent<SpriteRenderer>().sprite;
                yield return new WaitForSeconds(thoughtDelay);
            }
        }

        acceptingInput = true;
    }

    public void ClickedFood(Vector2 worldPoint)
    {
        if (!acceptingInput || thoughtQueue.Count == 0) return;

        Collider2D hit = Physics2D.OverlapPoint(worldPoint);
        if (hit != null)
        {
            FoodItem food = hit.GetComponent<FoodItem>();
            if (food != null && food.itemID == thoughtQueue.Peek())
            {
                thoughtQueue.Dequeue();
                currentFoods.Remove(food);
                Destroy(food.gameObject);

                if (thoughtQueue.Count == 0)
                {
                    // Speed up and start next round
                    thoughtDelay = Mathf.Max(0.5f, thoughtDelay - 0.2f);
                    Invoke(nameof(StartNewRound), 1f);
                }
            }
        }
    }
}
