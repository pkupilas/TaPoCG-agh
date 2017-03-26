using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
