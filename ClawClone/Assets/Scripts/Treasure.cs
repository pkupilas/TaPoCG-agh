using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Treasure : Item
{

    [SerializeField]
    private int _points = 1;
    [SerializeField]
    private bool _isKinematicAndTrigger = true;          // Defines how the treasure's rigidbody behaves. Set in inspector.

    void Awake()
    {
        SetRigidbody();
    }

    private void SetRigidbody()
    {
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        var boxCollider = gameObject.GetComponent<BoxCollider2D>();

        if (_isKinematicAndTrigger)
        {
            rigidBody.bodyType = RigidbodyType2D.Kinematic;
            boxCollider.isTrigger = true;
        }
        else
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            boxCollider.isTrigger = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            base.OnTriggerEnter2D(other);
            player.GainPoints(_points);
        }
    }
    
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            base.OnCollisionEnter2D(other);
            player.GainPoints(_points);
        }
    }

    // TODO: Implement when UI will be created.
    private void AddPoints()
    {
        Debug.Log("+" + _points + " Points!");
    }

}
