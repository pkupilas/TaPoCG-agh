using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    public Item[] spawningItems;

    // Used in BoxDestroy animation as animation event
    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}
