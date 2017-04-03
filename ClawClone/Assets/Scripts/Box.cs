using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public Item[] spawningItems;

    // Used in BoxDestroy animation as animation event 
    private void SpawnRandomItem()
    {
        if (spawningItems.Length>0)
        {
            int randomIndex = Random.Range(0, spawningItems.Length);
            var boxPosition = transform.position;
            Instantiate(spawningItems[randomIndex], boxPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("SpawningItems array is not initialized in inspector.");
        }
    }

    // Used in BoxDestroy animation as animation event
    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}
