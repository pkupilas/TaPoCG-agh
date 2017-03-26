using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Item
{

    [SerializeField] private int points;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Debug.Log("+" + points + " Points!");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>()!=null)
        {
            base.OnCollisionEnter2D(other);
        }
        Debug.Log("Collision entered.");
    }
}
