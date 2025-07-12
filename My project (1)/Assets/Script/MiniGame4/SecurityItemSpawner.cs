using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityItemSpawner : MonoBehaviour
{
    public GameObject itemGroupPrefab;
    public GameObject itemPrefab;
    public List<Sprite> legalSprites;
    public List<Sprite> illegalSprites;

    public Transform spawnFrom;
    public Transform centerPoint;

    public ItemGroup SpawnGroup()
    {
        GameObject groupObj = Instantiate(itemGroupPrefab, spawnFrom.position, Quaternion.identity);
        ItemGroup group = groupObj.GetComponent<ItemGroup>();

        int itemCount = Random.Range(3, 6);
        float clusterRadius = 1f;

        for (int i = 0; i < itemCount; i++)
        {
            GameObject itemObj = Instantiate(itemPrefab, groupObj.transform);
            Item item = itemObj.GetComponent<Item>();
            SpriteRenderer sr = itemObj.GetComponent<SpriteRenderer>();

            bool isIllegal = Random.value < 0.3f;
            item.isIllegal = isIllegal;
            sr.sprite = isIllegal
                ? illegalSprites[Random.Range(0, illegalSprites.Count)]
                : legalSprites[Random.Range(0, legalSprites.Count)];

            group.items.Add(item);

            // Clustered layout
            Vector2 offset = Random.insideUnitCircle * clusterRadius;
            itemObj.transform.localPosition = new Vector3(offset.x, offset.y, 0);
        }

        group.SlideIn(centerPoint.position);
        return group;
    }
}
