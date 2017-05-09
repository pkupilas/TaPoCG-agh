using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D platform;

	public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().ChangeOnLadder(true);
            platform.enabled = false;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().ChangeOnLadder(false);
            platform.enabled = true;
        }
    }

}
