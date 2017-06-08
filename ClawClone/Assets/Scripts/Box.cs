using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Used in BoxDestroy animation as animation event
    private void DestroyBox()
    {
        Destroy(gameObject);
    }
}
