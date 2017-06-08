using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Item[] spawningItems;

    void SpawnRandomItem()
    {
        if (spawningItems.Length > 0)
        {
            int randomIndex = Random.Range(0, spawningItems.Length);
            var boxPosition = transform.position;
            var item = Instantiate(spawningItems[randomIndex], boxPosition, Quaternion.identity);
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            item.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            Debug.LogWarning("SpawningItems array is not initialized in inspector.");
        }
    }
}

