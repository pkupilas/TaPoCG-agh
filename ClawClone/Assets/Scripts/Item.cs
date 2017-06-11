using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    // For Kinematic (not affected by gravity) objects
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
        }
    }

    // For Dynamic (affected by gravity) objects
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            Destroy(gameObject);
        }
    }
}
